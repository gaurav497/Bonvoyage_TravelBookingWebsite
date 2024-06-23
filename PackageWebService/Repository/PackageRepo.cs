//using PackageWebService.Models;

using PackageWebService.Models;

namespace PackageWebService.Repository
{
    public class PackageRepo : IPackageRepo
    {
        private BonvoyagePackageDbContext _BonvoyagePackageDbContext;
        public PackageRepo()
        {
            _BonvoyagePackageDbContext = new BonvoyagePackageDbContext();
        }
        public PackageRepo(BonvoyagePackageDbContext dbContext)
        {
            _BonvoyagePackageDbContext = dbContext;
        }

        public List<Package> GetAllPackage()
        {
            List<Package> packages = new List<Package>();
            try
            {
                packages = _BonvoyagePackageDbContext.Packages.ToList();

            }
            catch (Exception ex)
            {
                packages = null;

            }
            return packages;
        }

        public List<Package> GetPackage(string location)
        {
            try
            {
                List<Package> ans = (from p in _BonvoyagePackageDbContext.Packages
                                     where p.PackageCountry == location
                                     select p).ToList();
                return ans;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Package>();
            }
        }
        public List<string> GetWishList(string userId)
        {
            try
            {
                List<string> ans = (from w in _BonvoyagePackageDbContext.Wishlists
                                    where w.UserId == userId
                                    select w.PackageId).ToList();
                return ans;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>();
            }
        }
        public Package Onepackage(string packageId)
        {
            try
            {
                var q = (from p in _BonvoyagePackageDbContext.Packages
                         where p.PackageId == packageId
                         select p).FirstOrDefault();
                return q;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

      public bool UpdateWishList(string userId, string packageId)
        {
            try
            {
                Wishlist wishlist = new Wishlist();
                wishlist.UserId = userId;
                wishlist.PackageId = packageId;
                _BonvoyagePackageDbContext.Wishlists.Add(wishlist);
                _BonvoyagePackageDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteWishList(string userId, string packageId)
        {
            try
            {
                Wishlist wishlist = (from w in _BonvoyagePackageDbContext.Wishlists
                                     where w.UserId == userId && w.PackageId == packageId
                                     select w).FirstOrDefault();
                if (wishlist != null)
                {
                    _BonvoyagePackageDbContext.Wishlists.Remove(wishlist);
                    _BonvoyagePackageDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public List<Package> GetPackageForWishlist(string UserId)
        {
            try
            {
                var q = (from p in _BonvoyagePackageDbContext.Packages
                         join w in _BonvoyagePackageDbContext.Wishlists on p.PackageId equals w.PackageId
                         where w.UserId == UserId
                         select p).ToList();
                return q;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Package>();
            }
        }

        public bool AddToWishList(string userId, string packageId)
        {
            try
            {
                var wishlist = new Wishlist
                {
                    UserId = userId,
                    PackageId = packageId
                };
                _BonvoyagePackageDbContext.Wishlists.Add(wishlist);
                _BonvoyagePackageDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
