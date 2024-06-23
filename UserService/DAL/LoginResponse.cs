using UserService.Models;

namespace UserService.DAL
{
    public class LoginResponse
    {
        public string Status { get; set; }
        public int Results { get; set; }

        public Data Data { get; set; } = new Data();


    }
}
