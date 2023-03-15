using DocLibMan.Controllers;
using DocLibMan.Helpers;
using DocLibMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocLibManTests.Controllers
{
    [TestClass()]
    public class SearchControllerTests
    {
        private readonly Mock<IAzureSearch> _mockAzureSearch;
        private readonly SearchController _controller;
        
        public SearchControllerTests()
        {
            _mockAzureSearch = new Mock<IAzureSearch>();
            _controller = new SearchController(_mockAzureSearch.Object);
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