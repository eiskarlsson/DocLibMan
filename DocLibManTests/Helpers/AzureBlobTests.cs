using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocLibMan.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DocLibMan.Helpers.Tests
{
    [TestClass()]
    public class AzureBlobTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly IEnumerable<IFormFile> _mockFiles;

        public AzureBlobTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockFiles = new List<IFormFile>() { };

            _mockConfiguration.SetupGet(a => a["BlobStorageAccountName"]).Returns("TestAccount");
            _mockConfiguration.SetupGet(a => a["BlobStorageAccountKey"]).Returns("VGVzdEFjY291bnRLZXk=");
            _mockConfiguration.SetupGet(a => a["BlobServiceUrl"]).Returns("https://doclibman.test.net/test");
        }

        [TestMethod()]
        public void AzureBlobTest()
        {
            var result = new AzureBlob(_mockConfiguration.Object);
            Assert.IsInstanceOfType(result, typeof(AzureBlob));
        }

        [TestMethod()]
        public async Task UploadFilesToBlobWithIndexTagsAsyncTest()
        {
            var azureBlobObject = new AzureBlob(_mockConfiguration.Object);
            var intResult = await azureBlobObject.UploadFilesToBlobWithIndexTagsAsync(_mockFiles, It.IsAny<string>());
            Assert.IsInstanceOfType(intResult, typeof(int));
        }

        [TestMethod()]
        public void GetUrlTest()
        {
            var uri = "https://example.com:8042/over/there?name=ferret#nose";
            var url = "https://example.com:8042/over/there?name=ferret#nose";
            
            var result = AzureBlob.GetUrl(new Uri(uri));
            
            Assert.AreEqual(result, url);
        }
    }
}