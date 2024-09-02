using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using BookingWebService.Repository;
using BookingWebService.Controllers;
using Assert = Xunit.Assert;
using Microsoft.Extensions.Configuration;
using BookingWebService.Models;
using BookingWebService.Dal;

namespace BookingWebService.Controllers.Tests
{
    [TestClass()]
    public class BookingTest
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly BookingController _controller;

        private readonly Mock<IBookingRepo> _repositoryMock;

        public BookingTest()
        {
            _repositoryMock = new Mock<IBookingRepo>();
            _configMock = new Mock<IConfiguration>();
            _controller = new BookingController(_repositoryMock.Object, _configMock.Object);
        }

        [Fact]
        [TestMethod]
        public void GetAllBooking_Returns_JsonResult()
        {

            var bookings = new List<Booking>
            {
                new Booking { BookingId = "1", PackageName = "Mumbai" },
                new Booking { BookingId = "2", PackageName = "Delhi" }
            };

            _repositoryMock.Setup(repo => repo.GetAllBooking()).Returns(bookings);

            // Act 
            var result = _controller.GetAllBooking();

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<List<Booking>>(jsonResult.Value);
            Assert.Equal(2, model.Count);
        }

          [Fact]
          [TestMethod]
          public void GetBookingJson_Returns_JsonResult_With_Valid_UserI()
          {
               //Arrange
              string userId = "123";
              var bookings = new List<Booking>
              {              
                  new Booking { BookingId = "4", PackageName = "Mumbai" },
              };
            _repositoryMock.Setup(repo => repo.GetBooking(userId)).Returns(bookings);
            //Act
            var result = _controller.GetBookingJson(userId);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var response = Assert.IsType<GetBookingResponse>(jsonResult.Value);
            Assert.Equal(bookings, response.Bookings);

        }



    }
}
