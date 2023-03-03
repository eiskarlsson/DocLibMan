using Azure.Storage.Blobs;
using Azure.Storage;
using Microsoft.Extensions.Configuration;

namespace DocLibMan.Helpers
{
    public class AzureBlob
    {
        private readonly IConfiguration _configuration;

        public AzureBlob(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public async Task<int> UploadFilesToBlobWithIndexTagsAsync(IEnumerable<IFormFile> files, string description = "")
        {
            var containerName = "doclibman";

            // Connect using Storage Account Key(in portal: Settings->Access Keys->Key)
            string accountName = _configuration["BlobStorageAccountName"];
            string accountKey = _configuration["BlobStorageAccountKey"];
            // Blob service URL can be found in portal: Settings->Properties->Blob Service
            Uri serviceUri = new Uri(_configuration["BlobServiceUrl"]);
            StorageSharedKeyCredential credential = new StorageSharedKeyCredential(accountName, accountKey);
            BlobServiceClient blobServiceClient = new BlobServiceClient(serviceUri, credential);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            long size = files.Sum(f => f.Length);

            // full path to file in temp location (buffer file locally before uploading).
            // note: for larger files and workloads, consider streaming instead.
            var filePath = Path.GetTempFileName();

            int uploadFileCount = 0;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetFileName(formFile.FileName);
                    var fileExt = Path.GetExtension(formFile.FileName);

                    // create a local reference to a blob
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);

                    IDictionary<string, string> blobdetails = new Dictionary<string, string>();

                    blobdetails.Add("Description", description);
                    blobdetails.Add("FileExtension", fileExt);
                    blobdetails.Add("DateUploaded", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    blobdetails.Add("DownloadLink", AzureBlob.GetUrl(blobClient.Uri));

                    using (var stream = formFile.OpenReadStream())
                    {
                        // upload the blob to Azure Storage
                        await blobClient.UploadAsync(stream, true);
                        await blobClient.SetMetadataAsync(blobdetails);
                        // update index
                        await new AzureIndexer(_configuration).Run();
                        uploadFileCount++;
                    }
                }
            }

            return uploadFileCount;


        }


        public static string GetUrl(Uri uri) => uri?.IsAbsoluteUri == true ? uri?.AbsoluteUri : uri?.ToString();
    }
}
