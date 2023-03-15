using DocLibMan.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocLibManTests.Helpers
{
    [TestClass()]
    public class AzureIndexerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly IConfiguration _configuration;
        
        public AzureIndexerTests()
        {
            _configuration = ConfigurationSettings.InitConfiguration();
            _mockConfiguration = new Mock<IConfiguration>();
            
            _mockConfiguration.SetupGet(a => a["SearchServiceUri"]).Returns(_configuration["SearchServiceUri"]);
            _mockConfiguration.SetupGet(a => a["SearchServiceAdminApiKey"]).Returns(_configuration["SearchServiceAdminApiKey"]);
            _mockConfiguration.SetupGet(a => a["SearchIndexerName"]).Returns(_configuration["SearchIndexerName"]);
            
        }

        [TestMethod()]
        public void AzureIndexerTest()
        {
            var result = new AzureIndexer(_mockConfiguration.Object);
            Assert.IsInstanceOfType(result, typeof(AzureIndexer));
        }

        [TestMethod()]
        public async Task RunTest()
        {
            bool result = false;
            var azureIndexerObject = new AzureIndexer(_mockConfiguration.Object);
            try
            {
                result = await azureIndexerObject.Run();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got this: "+ex.Message);
            }
            Assert.IsTrue(result);
        }
    }
}