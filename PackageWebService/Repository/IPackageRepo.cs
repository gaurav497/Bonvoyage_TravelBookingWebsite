//using PackageWebService.Models;

using PackageWebService.Models;

namespace PackageWebService.Repository
{
    public interface IPackageRepo
    {
        public List<Models.Package> GetAllPackage();
        
        public List<Models.Package> GetPackage(string location);
        public List<string> GetWishList(string userId);
        public Package Onepackage(string packageId);
        public bool UpdateWishList(string userId, string packageId);
        public bool DeleteWishList(string userId, string packageId);
        public List<Package> GetPackageForWishlist(string UserId);

        public bool AddToWishList(string userId, string packageId);
    }
}
