using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using Azure.Storage.Blobs;
using DocLibMan.Helpers;
using DocLibMan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DocLibMan.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private IAzureSearch _azureSearch { get; }
        

        public SearchController(IAzureSearch azureSearch)
        {
            _azureSearch =  azureSearch;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View("Index", new DocLibManSearchModel() { SearchText = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Index(DocLibManSearchModel searchParam)
        {
            var result = new List<DocLibManDocument>();
            if (searchParam != null && !String.IsNullOrEmpty(searchParam.SearchText))
            {
                if (ModelState.IsValid)
                {
                    result = _azureSearch.Search(searchParam.SearchText).Result.ToList();
                }

            }
            var model = new DocLibManSearchModel()
            { SearchText = searchParam?.SearchText, SearchResults = result.ToList() };

            return View(model);
        }

    }
}



