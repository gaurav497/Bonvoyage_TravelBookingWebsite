using BookingWebService.Models;

namespace BookingWebService.Repository
{
    public interface IBookingRepo
    {
        public List<Booking> GetAllBooking();
        public List<Booking> GetBooking(string userId);
        public Booking UpdateBooking(Booking booking);
        public string GetNewUserId();

        public bool AddBooking(Booking booking);
    }
}
