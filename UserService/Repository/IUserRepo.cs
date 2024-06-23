using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepo
    {
        public List<User> GetAllUsers();
        public bool RegisterUser(User user);

        public User LoginUser(string userEmail, string userPassword);
        public User GetUser(string userName);
        public User GetUserById(string userId);
        public string GetNewUserId();
    }
}
