using DocLibMan.Controllers;
using DocLibMan.Helpers;
using DocLibMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocLibManTests.Controllers
{
    [TestClass()]
    public class HomeControllerTests
    {
        private readonly Mock<IAzureBlob> _mockAzureBlob;
        private readonly HomeController _controller;
        public HomeControllerTests()
        {
            _mockAzureBlob = new Mock<IAzureBlob>();
            _controller = new HomeController(_mockAzureBlob.Object);
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