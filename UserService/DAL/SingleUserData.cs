using UserService.Models;

namespace UserService.DAL
{
    public class SingleUserData
    {
        public User user{ get; set; }=new User();
        public List<string> WishList { get; set; } = new List<string>();
    }
}
