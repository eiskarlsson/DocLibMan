using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocLibMan.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocLibMan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DocLibMan.Controllers.Tests
{
    [TestClass()]
    public class SearchControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly SearchController _controller;
        
        public SearchControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new SearchController(_mockConfiguration.Object);
        }
        
        [TestMethod()]
        public void IndexTest()
        {
            var result = _controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult)?.Model, typeof(DocLibManSearchModel));
        }

        [TestMethod()]
        public void IndexTestPost()
        {
            var result = _controller.Index(new DocLibManSearchModel(){SearchText = It.IsAny<string>(), SearchResults = null});
            Assert.IsInstanceOfType(result, typeof(IActionResult));
            Assert.IsInstanceOfType((result as ViewResult)?.Model, typeof(DocLibManSearchModel));
        }
    }
}