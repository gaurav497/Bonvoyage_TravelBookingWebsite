using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PackageWebService.Models;
using System.Security.Cryptography;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepo : IUserRepo
    {
        private BonvoyageUsersDbContext _BonvoyageUsersDbContext;
        public UserRepo()
        {
            _BonvoyageUsersDbContext = new BonvoyageUsersDbContext();
        }
        public UserRepo(BonvoyageUsersDbContext dbContext)
        {
            _BonvoyageUsersDbContext = dbContext;
        }

        public bool RegisterUser(User user)
        {
      
            try
            {
                
                
                _BonvoyageUsersDbContext.Add(user);
                _BonvoyageUsersDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public List<User> GetAllUsers()
        {
            List<User> Users = new List<User>();
            try
            {
                Users = _BonvoyageUsersDbContext.Users.ToList();

            }
            catch (Exception ex)
            {
                Users = null;

            }
            return Users;
        }


        public User LoginUser(string userEmail, string userPassword)
        {
            try
            {
                var user = (from u in _BonvoyageUsersDbContext.Users
                            where u.UserEmail == userEmail && u.UserPassword == userPassword
                            select u).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public User GetUser(string userName)
        {
            try
            {
                var q = (from b in _BonvoyageUsersDbContext.Users
                         where b.UserName == userName
                         select b).FirstOrDefault();
                return q;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public User GetUserById(string userId)
        {
            try
            {
                var q = (from b in _BonvoyageUsersDbContext.Users
                         where b.UserId == userId
                         select b).FirstOrDefault();
                return q;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public string GetNewUserId()
        {
            try
            {
                string q = _BonvoyageUsersDbContext.Users
                                .OrderByDescending(item => item.UserId)
                                .Select(item => item.UserId)
                                .FirstOrDefault();

                if (q != null)
                {
                    int number = int.Parse(q.Substring(1)); 
                    number++;

                    
                    string newUserId = $"U{number:D3}";
                    return newUserId;
                }
                else
                {
                    
                    return "U001";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; 
            }

        }
        public string RefreshTokenGenerator()
        {
            
                var randomNumber = new byte[64];
                using (var numberGenerator = RandomNumberGenerator.Create())
                {
                    numberGenerator.GetBytes(randomNumber);
                }

            RefreshTokenClass refreshTokenClass = new RefreshTokenClass();
            String token = Convert.ToBase64String(randomNumber);
            refreshTokenClass.RefreshTOken = token;
            refreshTokenClass.RefreshTokenExpirationTime = DateTime.Now.AddMinutes(5);
        _BonvoyageUsersDbContext.RefreshTokensClasses.Add(refreshTokenClass);   
        _BonvoyageUsersDbContext.SaveChanges();
        return token;
        }
        public bool CheckValidityOfRefreshToken(string refeshToken)
        {
            var storeRefreshToken = (from r in _BonvoyageUsersDbContext.RefreshTokensClasses
                                     where r.RefreshTOken == refeshToken
                                     select r).FirstOrDefault();
          
            if (storeRefreshToken != null)
            {

                if(storeRefreshToken.RefreshTokenExpirationTime < DateTime.Now)
                {
                    Console.WriteLine("expired");
                    return false;
                }
                else
                {
                    Console.WriteLine("active");
                   
                }

            }

            if (storeRefreshToken!=null)
            {
                return true;
            }
            return false;
        }




    }
}
