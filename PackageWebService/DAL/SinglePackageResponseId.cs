using PackageWebService.Models;

namespace PackageWebService.DAL
{
    public class SinglePackageResponseId
    {
        public string Status { get; set; }
        public int Result { get; set; }
        public Package data { get; set; } = new Package();
    }
}
