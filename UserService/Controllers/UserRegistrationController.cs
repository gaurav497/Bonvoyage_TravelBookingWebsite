using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DAL;
using UserService.Models;
using UserService.Repository;
using UserService.ServiceDiscovery;
using System.Net.Http.Json;

namespace UserService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConsulClient _consulClient;

        public UserRegistrationController(IUserRepo repo, IHttpClientFactory httpClientFactory, IConsulClient consulClient)
        {
            _userRepo = repo;
            _consulClient = consulClient;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public JsonResult GetAllUserResult()
        {
            return new JsonResult(_userRepo.GetAllUsers());
        }

        [HttpPost]
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
        public async Task<ActionResult> Login(string useremail, string password)
        {
            User user = _userRepo.LoginUser(useremail, password);

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

            LoginResponse response = new LoginResponse();
            response.Status = "success";
            response.Results = 1;

            response.Data.DalUser.User = user;
            response.Data.DalUser.UserWishlist = wishlist;
            response.Data.Booking = booking;

            return new JsonResult(response);
        }


        [HttpGet("{userName}")]
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

    } 
}
