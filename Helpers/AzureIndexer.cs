using Azure;
using Azure.Search.Documents.Indexes;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;

namespace DocLibMan.Helpers
{

    public class AzureIndexer
    {
        private readonly IConfiguration _configuration;

        public AzureIndexer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Run()
        {
            string searchServiceUri = _configuration["SearchServiceUri"];
            string adminApiKey = _configuration["SearchServiceAdminApiKey"];
            string blobIndexerName = _configuration["SearchIndexerName"];

            //SearchIndexClient indexClient = new SearchIndexClient(new Uri(searchServiceUri), new AzureKeyCredential(adminApiKey));
            SearchIndexerClient indexerClient = new SearchIndexerClient(new Uri(searchServiceUri), new AzureKeyCredential(adminApiKey));

            try
            {
                var response = await indexerClient.RunIndexerAsync(blobIndexerName);
                return !response.IsError;
            }
            catch (RequestFailedException ex) when (ex.Status == 429)
            {
                Debug.WriteLine("Failed to run indexer: {0}", ex.Message);
                return false;
            }
        }
    }
}
