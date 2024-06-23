using UserService.Models;

namespace UserService.DAL
{
    public class Data
    {
        public DalUser DalUser { get; set; } = new DalUser();
        public List<Booking> Booking { get; set; }=new List<Booking>();


    }
}
