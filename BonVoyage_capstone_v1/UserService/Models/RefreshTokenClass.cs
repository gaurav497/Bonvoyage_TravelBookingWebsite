using System.ComponentModel.DataAnnotations;

namespace PackageWebService.Models
{
    public class RefreshTokenClass
    {
        [Key]
        public string  RefreshTOken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
