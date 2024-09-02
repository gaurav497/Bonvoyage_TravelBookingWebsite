using PackageWebService.Models;

namespace PackageWebService.DAL
{
    public class PackageResponse
    {
        public string Status { get; set; }
        public int Result { get; set; }
        public List<Package> data { get; set; }=new List<Package>();
    }
}
