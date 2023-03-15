using Azure.Search.Documents;
using Azure;
using DocLibMan.Models;
using Azure.Search.Documents.Models;
using System.Net;
using System.Web;
using Azure.Storage.Blobs;

namespace DocLibMan.Helpers
{
    public interface IAzureSearch
    {
        public Task<IEnumerable<DocLibManDocument>> Search(string query);
    }

    public class AzureSearch : IAzureSearch
    {
        private readonly IConfiguration _configuration;

        public AzureSearch(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public async Task<IEnumerable<DocLibManDocument>> Search(string query)
        {
            //var urlEncodedQuery = HttpUtility.UrlEncode(query);
            SearchClient searchClient = CreateSearchClientForQueries(_configuration["SearchIndexName"]);
            SearchOptions options = new SearchOptions() { IncludeTotalCount = true, QueryType= SearchQueryType.Simple};
            var results = await searchClient.SearchAsync<DocLibManDocument>(query, options);

            List<DocLibManDocument> documents = new List<DocLibManDocument>();

            foreach (var s in results.Value.GetResults())
            {
                documents.Add(s.Document);
            }
            return documents;
        }
        
        private SearchClient CreateSearchClientForQueries(string indexName)
        {
            string searchServiceEndPoint = _configuration["SearchServiceUri"];
            string queryApiKey = _configuration["SearchServiceAdminApiKey"];

            SearchClient searchClient = new SearchClient(new Uri(searchServiceEndPoint), indexName, new AzureKeyCredential(queryApiKey));
            return searchClient;
        }

        
    }
}
