using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DocLibMan.Helpers;
using DocLibMan.Models;
using Kendo.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace DocLibMan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Admin(IEnumerable<IFormFile> files, string description)
        {
            IEnumerable<string> fileInfo = new List<string>();

            if (files != null)
            {
                if (ModelState.IsValid)
                {
                    fileInfo = GetFileInfo(files);
                    try
                    {
                        await new AzureBlob(_configuration).UploadFilesToBlobWithIndexTagsAsync(files, description);
                    }
                    catch (Exception ex)
                    {
                        //Log exception instead of throw
                        Debug.WriteLine("Upload to Blob failed {0}", ex.Message);
                    }
                }
            }

            return View("Admin", fileInfo);
        }

        private IEnumerable<string> GetFileInfo(IEnumerable<IFormFile> files)
        {
            List<string> fileInfo = new List<string>();

            foreach (var file in files)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));

                fileInfo.Add(string.Format("{0} ({1} bytes)", fileName, file.Length));
            }

            return fileInfo;
        }

        

       
    }
}
