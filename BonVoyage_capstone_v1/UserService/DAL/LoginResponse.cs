using UserService.Models;

namespace UserService.DAL
{
    public class LoginResponse
    {
        public string Status { get; set; }
        public int Results { get; set; }

        public Data Data { get; set; } = new Data();
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
