using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DAL;
using UserService.Models;
using UserService.Repository;
using UserService.ServiceDiscovery;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;

namespace UserService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConsulClient _consulClient;
        private IConfiguration _config;

        public UserRegistrationController(IUserRepo repo, IHttpClientFactory httpClientFactory, IConsulClient consulClient, IConfiguration config)
        {
            _userRepo = repo;
            _consulClient = consulClient;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetAllUserResult()
        {
            return new JsonResult(_userRepo.GetAllUsers());
        }
       
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(User user)
        {
            List<Booking> booking = new List<Booking>();
            List<string> wishlist = new List<string>();

            #region Booking Service API Fetch
            var bookingServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "booking-service");
            if (bookingServiceUri == null)
            {
                return NotFound();
            }
            var bookingHttpClient = _httpClientFactory.CreateClient();
            bookingHttpClient.BaseAddress = bookingServiceUri;
            var bookingApiResponse = await bookingHttpClient.GetAsync($"api/Booking/GetBooking/{user.UserId}");
            if (bookingApiResponse.IsSuccessStatusCode)
            {
                booking = await bookingApiResponse.Content.ReadFromJsonAsync<List<Booking>>();
            }
            #endregion 

            #region Package Service API Fetch
            var packageServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "package-service");
            if (packageServiceUri == null)
            {
                return NotFound();
            }
            var packageHttpClient = _httpClientFactory.CreateClient();
            packageHttpClient.BaseAddress = packageServiceUri;
            var packageApiResponse = await packageHttpClient.GetAsync($"api/Package/GetWishList/{user.UserId}");
            if (packageApiResponse.IsSuccessStatusCode)
            {
                wishlist = await packageApiResponse.Content.ReadFromJsonAsync<List<string>>();
            }
            #endregion
            UserResponse response = new UserResponse();
            response.Status = "Failed";
            if (_userRepo.RegisterUser(user))
            {
                response.Status = "success";
                response.Data.DalUser.UserWishlist = wishlist;
                response.Data.DalUser.User = user;
                response.Data.Booking = booking;
            }
            return new JsonResult(response);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string useremail, string password)
        {
            LoginResponse response = new LoginResponse();
            User user = _userRepo.LoginUser(useremail, password);
            
            if (user == null)
            {
                return new JsonResult("User Not available");
            }
            else
            {
                var tokenString = GenerateJSONWebToken();
                if (tokenString != null)
                {
                    response.Token = tokenString;
                }

                var refreshString = _userRepo.RefreshTokenGenerator();
                if (refreshString != null)
                {
                    response.RefreshToken= refreshString;
                }

            }
            List<Booking> booking = new List<Booking>();
            List<string> wishlist = new List<string>();

            #region Booking Service API Fetch
            var bookingServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "booking-service");
            if (bookingServiceUri == null)
            {
                return NotFound();
            }
            var bookingHttpClient = _httpClientFactory.CreateClient();
            bookingHttpClient.BaseAddress = bookingServiceUri;
            var bookingApiResponse = await bookingHttpClient.GetAsync($"api/Booking/GetBooking/{user.UserId}");
            if (bookingApiResponse.IsSuccessStatusCode)
            {
                booking = await bookingApiResponse.Content.ReadFromJsonAsync<List<Booking>>();
            }
            #endregion 


            #region Package Service API Fetch
            var packageServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "package-service");
            if (packageServiceUri == null)
            {
                return NotFound();
            }
            var packageHttpClient = _httpClientFactory.CreateClient();
            packageHttpClient.BaseAddress = packageServiceUri;
            var packageApiResponse = await packageHttpClient.GetAsync($"api/Package/GetWishList/{user.UserId}");
            if (packageApiResponse.IsSuccessStatusCode)
            {
                wishlist = await packageApiResponse.Content.ReadFromJsonAsync<List<string>>();
            }
            #endregion

            
            response.Status = "success";
            response.Results = 1;

            response.Data.DalUser.User = user;
            response.Data.DalUser.UserWishlist = wishlist;
            response.Data.Booking = booking;

            return new JsonResult(response);
        }
        
       

        [HttpGet("{userName}")]
        [Authorize]
        public async Task<ActionResult>user(string userName)
        {

            List<string> wishlist = new List<string>();
            User u=_userRepo.GetUser(userName);
            SingleUser response = new SingleUser();

            var packageServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "package-service");
            if (packageServiceUri == null)
            {
                return NotFound();
            }
            var packageHttpClient = _httpClientFactory.CreateClient();
            packageHttpClient.BaseAddress = packageServiceUri;
            var packageApiResponse = await packageHttpClient.GetAsync($"api/Package/GetWishList/{u.UserId}");
            if (packageApiResponse.IsSuccessStatusCode)
            {
                wishlist = await packageApiResponse.Content.ReadFromJsonAsync<List<string>>();
            }
            response.Status = "success";
            response.Result = 1;
            response.user.user = u;
            response.user.WishList = wishlist;
            return new JsonResult(response);
        }

        [HttpPut("{userId}/{packageId}")]
        [Authorize]
        public async Task<ActionResult> UpdateWishList(string userId,string packageId)
        {
            List<Booking>bookings = new List<Booking>();
            List<string> wishlist = new List<string>();
            bool ans = false;
            var packageServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "package-service");
            if (packageServiceUri == null)
            {
                return NotFound();
            }
            var packageHttpClient = _httpClientFactory.CreateClient();
            packageHttpClient.BaseAddress = packageServiceUri;
            var packageApiResponseUpdate = await packageHttpClient.GetAsync($"api/Package/UpdateWishList/{userId}/{packageId}");
            if (packageApiResponseUpdate.IsSuccessStatusCode)
            {
                ans = await packageApiResponseUpdate.Content.ReadFromJsonAsync<bool>();
            }
            var packageApiResponse= await packageHttpClient.GetAsync($"api/Package/GetWishList/{userId}");
            if (packageApiResponse.IsSuccessStatusCode)
            {
                wishlist= await packageApiResponse.Content.ReadFromJsonAsync<List<string>>();
            }

            Booking book = new Booking();

            var bookingServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "booking-service");
            if (bookingServiceUri == null)
            {
                return NotFound();
            }
            var bookingHttpClient = _httpClientFactory.CreateClient();
            bookingHttpClient.BaseAddress = bookingServiceUri;
            var bookingApiResponse = await bookingHttpClient.GetAsync($"api/Booking/GetBooking/{userId}");
            if (bookingApiResponse.IsSuccessStatusCode)
            {
                book= await bookingApiResponse.Content.ReadFromJsonAsync<Booking>();
            }
            WishListUpdateResponse response = new WishListUpdateResponse();
            response.Status = "success";
            response.data.UserWishlist = wishlist;
            response.data.User=_userRepo.GetUserById(userId);
            bookings.Add(book);
            response.data.Bookings = bookings;
            return new JsonResult(response);
        }

        [HttpDelete("{userId}/{packageId}")]
        [Authorize]
        public async Task<ActionResult> DeleteWishList(string userId,string packageId)
        {
            List<Booking> bookings = new List<Booking>();
            List<string> wishlist = new List<string>();
            bool ans = false;
            var packageServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "package-service");
            if (packageServiceUri == null)
            {
                return NotFound();
            }
            var packageHttpClient = _httpClientFactory.CreateClient();
            packageHttpClient.BaseAddress = packageServiceUri;
            var packageApiResponseUpdate = await packageHttpClient.DeleteAsync($"api/Package/DeleteWishList/{userId}/{packageId}");
            if (packageApiResponseUpdate.IsSuccessStatusCode)
            {
                ans = await packageApiResponseUpdate.Content.ReadFromJsonAsync<bool>();
            }
            var packageApiResponse = await packageHttpClient.GetAsync( $"api/Package/GetWishList/{userId}");
            if (packageApiResponse.IsSuccessStatusCode)
            {
                wishlist = await packageApiResponse.Content.ReadFromJsonAsync<List<string>>();
            }

            Booking book = new Booking();

            var bookingServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "booking-service");
            if (bookingServiceUri == null)
            {
                return NotFound();
            }
            var bookingHttpClient = _httpClientFactory.CreateClient();
            bookingHttpClient.BaseAddress = bookingServiceUri;
            var bookingApiResponse = await bookingHttpClient.GetAsync($"api/Booking/GetBooking/{userId}");
            if (bookingApiResponse.IsSuccessStatusCode)
            {
                book = await bookingApiResponse.Content.ReadFromJsonAsync<Booking>();
            }
            WishListUpdateResponse response = new WishListUpdateResponse();
            response.Status = "success";
            response.data.UserWishlist = wishlist;
            response.data.User = _userRepo.GetUserById(userId);
            bookings.Add(book);
            response.data.Bookings = bookings;
            return new JsonResult(response);

        }

        [HttpGet]
        public async Task<ActionResult> Admin()
        {
            List<Booking> bookings = new List<Booking>();
            AdminResponse response=new AdminResponse();

            var bookingServiceUri = await ConsulServiceDiscovery.GetServiceUri(_consulClient, "booking-service");
            if (bookingServiceUri == null)
            {
                return NotFound();
            }
            var bookingHttpClient = _httpClientFactory.CreateClient();
            bookingHttpClient.BaseAddress = bookingServiceUri;
            var bookingApiResponse = await bookingHttpClient.GetAsync($"api/Booking/GetAllBooking");
            if (bookingApiResponse.IsSuccessStatusCode)
            {
               bookings = await bookingApiResponse.Content.ReadFromJsonAsync<List<Booking>>();
            }


            response.Status = "success";
            response.result = 1;
            response.data.Booking = bookings;
            return new JsonResult(response);
        }

        [HttpGet]
        public JsonResult GetNewUserId()
        {
            return new JsonResult(_userRepo.GetNewUserId());
        }


        [HttpGet]
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet]
        public JsonResult NewAccessToken(string refreshToken)
        {
            bool ans = _userRepo.CheckValidityOfRefreshToken(refreshToken);
            AccessAndRefreshResponse response = new AccessAndRefreshResponse();
           
            if (ans == true)
            {
                response.AccessToken = GenerateJSONWebToken();
                response.RefreshToken = _userRepo.RefreshTokenGenerator();
                return new JsonResult(response);
            }
            else
            {
                return new JsonResult("Not valid ");
            }
        }

      

    } 
}
