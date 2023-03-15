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
using System.Diagnostics;
using Kendo.Mvc.UI;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DocLibMan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAzureBlob _azureBlob;

        public HomeController(IAzureBlob azureBlob)
        {
            _azureBlob = azureBlob;
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
        public async Task<IActionResult> Admin(AdminModel model)
        {
            IEnumerable<string> fileInfo = new List<string>();

            if (model.Files != null)
            {
                if (ModelState.IsValid)
                {
                    fileInfo = GetFileInfo(model.Files);
                    try
                    {
                        await _azureBlob.UploadFilesToBlobWithIndexTagsAsync(model.Files, model.Description);
                    }
                    catch (Exception ex)
                    {
                        //Log exception instead of throw
                        Debug.WriteLine("Upload to Blob failed {0}", ex.Message);
                    }
                }
            }

            return View("Admin", new AdminModel() {FileInfo = fileInfo,  Description = model.Description, Files = model.Files});
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
