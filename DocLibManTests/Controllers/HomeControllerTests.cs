using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocLibMan.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using DocLibMan.Helpers;
using DocLibMan.Models;

namespace DocLibMan.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly IEnumerable<IFormFile> _mockFiles;
        private readonly HomeController _controller;
        public HomeControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockFiles = new List<IFormFile>() { };
            _controller = new HomeController(_mockConfiguration.Object);
        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = _controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void AdminTest()
        {
            var result = _controller.Admin();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void AdminTestPost()
        {
            var result = _controller.Admin(It.IsAny<AdminModel>());
            Assert.IsInstanceOfType(result, typeof(Task<IActionResult>));
        }
    }
}