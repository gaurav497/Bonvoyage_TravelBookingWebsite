using UserService.Models;

namespace UserService.DAL
{
    public class DalUser
    {
        public List<string> UserWishlist { get; set; }=new List<string>();

        public User User { get; set; } = new User();
    }
}
