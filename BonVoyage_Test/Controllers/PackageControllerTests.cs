using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using Assert = Xunit.Assert;
using PackageWebService.Repository;
using PackageWebService.Controllers;
using Consul;
using PackageWebService.Models;
using PackageWebService.DAL;

namespace PackageWebService.Controllers.Tests
{
    [TestClass()]
    public class PackageControllerTests
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly PackageController _controller;
        private readonly Mock<IPackageRepo> _repositoryMock;

        public PackageControllerTests()
        {
            _repositoryMock = new Mock<IPackageRepo>();
            _configMock = new Mock<IConfiguration>();
            _controller = new PackageController(_repositoryMock.Object, _configMock.Object);
        }


        [Fact]
        [TestMethod]
        public void GetWishListResults_ReturnsJsonResult_()
        {
            // Arrange
            var userId = "TestUserId";
            var ans = new List<string>{ "Package1", "Package2" };

            _repositoryMock.Setup(repo => repo.GetWishList(userId)).Returns(ans);

            // Act
            var result = _controller.GetWishList(userId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<List<string>>(jsonResult.Value);
            Assert.Equal(ans.Count, model.Count);
            Assert.Contains("Package1", model);
            Assert.Contains("Package2", model);
        }
        [Fact]
        [TestMethod()]
        public void GetPackage_ReturnsJsonResult_()
        {
            // Arrange
            var response = new List<Package>
            {
                new Package { PackageId = "1", PackageName = "Test1" },
                new Package { PackageId = "2", PackageName = "Test2" }
            };

            _repositoryMock.Setup(repo => repo.GetAllPackage()).Returns(response);

            // Act
            var result = _controller.GetPackages();

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var packageresponse = Assert.IsAssignableFrom<PackageResponse>(jsonResult.Value);
            Assert.Equal("success", packageresponse.Status);
            Assert.Equal(response.Count, packageresponse.Result);
            Assert.Equal(response, packageresponse.data);
        }

        [Fact]
        [TestMethod()]
        public void Delete_ReturnsJsonResult()
        {
            // Arrange
            var userId = "Normal_userId";
            var packageId = "Normal_packageId";
            _repositoryMock.Setup(repo => repo.DeleteWishList(userId,packageId)).Returns(true);

            // Act
            var result = _controller.DeleteWishList(userId,packageId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<bool>(jsonResult.Value);
            Assert.True(model);
        }
        [Fact]
        [TestMethod()]
        public void Update_ReturnsJsonResult_()
        {
            // Arrange
            var userId = "Normal_TestID";
            var packageId = "Normal_TestPackageId";
            _repositoryMock.Setup(repo => repo.UpdateWishList(userId,packageId)).Returns(true);

            // Act
            var result = _controller.UpdateWishList(userId,packageId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<bool>(jsonResult.Value);
            Assert.True(model);
        }
    }
}