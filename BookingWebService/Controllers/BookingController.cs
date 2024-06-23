using BookingWebService.Dal;
using BookingWebService.Models;
using BookingWebService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookingWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingRepo _bookingRepo;
        private IConfiguration _configuration;
        public BookingController(IBookingRepo bookingRepo, IConfiguration configuration)
        {
            _bookingRepo = bookingRepo;
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult GetAllBooking()
        {
            List<Booking> bookings=new List<Booking>();
            try
            {
                 bookings= _bookingRepo.GetAllBooking();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not able to Get Bookings");
            }
            return new JsonResult(bookings);
        }

        [HttpGet("{userId}")]
        public JsonResult GetBooking(string userId)
        {
            List<Booking>bookings = _bookingRepo.GetBooking(userId);
            GetBookingResponse response = new GetBookingResponse();
            response.Bookings = bookings;
            return new JsonResult(bookings);
        }
        [HttpPut]
        public JsonResult Bookings(Booking booking)
        {
            Booking book = new Booking();
            try
            {
                book=_bookingRepo.UpdateBooking(booking);

            } catch(Exception ex)
            {
                Console.WriteLine("not able to update Booking");
            }       

            return new JsonResult(book);
        }

        [HttpGet]
        public JsonResult GetNewBookingID()
        {
            string id="";
            try
            {
                id = _bookingRepo.GetNewUserId();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Not able To Generate");
            }
            return new JsonResult(id);
        }
        [HttpPost]
        public JsonResult AddBooking(Booking booking) {
            bool result=false;
            try
            {
                result = _bookingRepo.AddBooking(booking);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add booking failed");
            }
            return new JsonResult(result);
        }
        [HttpGet("{userId}")]
        public JsonResult GetBookingJson(string userId)
        {
            try
            {
                GetBookingResponse response = new GetBookingResponse();
                response.Bookings = _bookingRepo.GetBooking(userId);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Success = false,
                    Message = "An error occurred while processing your request."
                };
                return new JsonResult(errorResponse)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
