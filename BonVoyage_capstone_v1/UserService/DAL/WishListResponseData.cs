using UserService.Models;

namespace UserService.DAL
{
    public class WishListResponseData
    {
        public List<string> UserWishlist { get; set; } = new List<string>();
        public User User { get; set; } = new User();
        public List<Booking> Bookings { get; set; }= new List<Booking>();
    }
}
