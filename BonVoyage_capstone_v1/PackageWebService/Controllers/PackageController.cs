using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackageWebService.DAL;
using PackageWebService.Models;
using PackageWebService.Repository;

namespace PackageWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private IPackageRepo _packageRepo;
        private IConfiguration _configuration;
        public PackageController(IPackageRepo packageRepo, IConfiguration configuration)
        {
            _packageRepo = packageRepo;
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetPackages()
        {

            PackageResponse response = new PackageResponse();
            List<Package> ans = new List<Package>();
            try
            {
                ans = _packageRepo.GetAllPackage();
                response.Status = "success";
                response.Result = ans.Count();
                response.data = ans;
            }catch(Exception ex)
            {
                Console.WriteLine("Not able to get package");
            }
            return new JsonResult(response);
        }

        [HttpGet("{param:alpha}")]
        public JsonResult GetPackage(string param)
        {
            PackageResponse response = new PackageResponse();
            List<Package> ans = new List<Package>();
            try
            {

                ans = _packageRepo.GetPackage(param);
                response.Status = "success";
                response.Result = ans.Count();
                response.data = ans;
            }catch(Exception ex) {
                Console.WriteLine("not able to fetch package");
            }
            return new JsonResult(response);
        }
        [HttpGet("{param}")]
        public JsonResult OnePackage(string param)
        {
           SinglePackageResponseId respone= new SinglePackageResponseId();
            try
            {
                respone.Status = "success";
                respone.Result = 1;
                respone.data = _packageRepo.Onepackage(param);
            }
            catch (Exception ex) {
                Console.WriteLine("not able to get package");
            }
            return new JsonResult(respone);

        }
        [HttpGet("{userId}")]
        public JsonResult GetWishList(string userId)
        {
            List<string> whislists = new List<string>();
            try
            {
                whislists = _packageRepo.GetWishList(userId);
            }catch(Exception ex)
            {
                Console.WriteLine("not able to get whislist");
            }

            return new JsonResult(whislists);


        }
        [HttpPut("{userId}/{packageId}")]
        public JsonResult UpdateWishList(string userId,string packageId)
        {
            bool ans=false;
            try
            {
                ans = _packageRepo.UpdateWishList(userId, packageId);
            }
            catch (Exception ex) {
                Console.WriteLine("not able to update whislist");
            }

           return new JsonResult(ans);
        }
        [HttpDelete("{userId}/{packageId}")]
        public JsonResult DeleteWishList(string userId,string packageId)
        {
            bool ans=false;
            try
            {
                ans = _packageRepo.DeleteWishList(userId, packageId);
            }
            catch (Exception ex) {
                Console.WriteLine("Not able to delete whislist");
            }
            return new JsonResult(ans);   
        }
        [HttpGet("{userId}")]
        public JsonResult WishList(string userId)
        {
            List<Package> packages = new List<Package>();
            try
            {
                packages = _packageRepo.GetPackageForWishlist(userId);
            }catch(Exception ex)
            {
                Console.WriteLine("not able to fetch packages");
            }

            return new JsonResult(packages);
        }
        [HttpPost("{userId}/{packageId}")]
        public JsonResult AddToWishList(string userId,string packageId)
        {
            bool ans = false;
            try
            {
                ans = _packageRepo.AddToWishList(userId, packageId);

            }
            catch (Exception ex) {
                Console.WriteLine("not added to whislist");
            }
            return new JsonResult(ans);
        }


    }
}
