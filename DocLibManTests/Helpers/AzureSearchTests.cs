using DocLibMan.Helpers;
using DocLibMan.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocLibManTests.Helpers
{
    [TestClass()]
    public class AzureSearchTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        
        public AzureSearchTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            
            _mockConfiguration.SetupGet(a => a["BlobStorageAccountName"]).Returns("TestAccount");
            _mockConfiguration.SetupGet(a => a["BlobStorageAccountKey"]).Returns("VGVzdEFjY291bnRLZXk=");
            _mockConfiguration.SetupGet(a => a["BlobServiceUrl"]).Returns("https://doclibman.test.net/test");
        }
        
        [TestMethod()]
        public void AzureSearchTest()
        {
            var result = new AzureSearch(_mockConfiguration.Object);
            Assert.IsInstanceOfType(result, typeof(AzureSearch));
        }
        
        [TestMethod()]
        public void SearchTest()
        {
            var azureSearchObject = new AzureSearch(_mockConfiguration.Object);
            var result=azureSearchObject.Search(It.IsAny<string>());
            Assert.IsInstanceOfType(result, typeof(Task<IEnumerable<DocLibManDocument>>));
        }
        
    }
}