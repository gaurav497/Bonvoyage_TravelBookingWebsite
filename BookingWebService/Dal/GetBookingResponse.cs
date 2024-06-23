using BookingWebService.Models;

namespace BookingWebService.Dal
{
    public class GetBookingResponse
    {
        public List<Booking> Bookings { get; set; }=new List<Booking>();
    }
}
