using Alex.MinimalApi.Service.Core;
using Azure.Storage.Blobs;
namespace Alex.MinimalApi.Service.Infrastructure
{
    /// <summary>
    /// Azure Blob Storage service
    /// </summary>
    public class AzureBlobPublicDocumentService : IPublicDocumentService
    {
        private string _accountName;
        private string _accountKey;
        private string _container;
        private string _host;

        public AzureBlobPublicDocumentService(string accountName, string accountKey, string container, string host)
        {

            this._accountName = accountName;
            this._accountKey = accountKey;
            this._container = container;
            this._host = host;

        }

        public async Task<string> CreateAsync(Stream content)
        {
            //docid
            string docId = GetUniqueDocumentId();

            //connect
            var con = $"DefaultEndpointsProtocol=http;AccountName={_accountName};AccountKey={_accountKey};BlobEndpoint={_host}/{_accountName};";
            BlobServiceClient serviceClient = new BlobServiceClient(con);
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(_container);
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            //upload
            BlobClient blobClient = containerClient.GetBlobClient(docId);
            await blobClient.UploadAsync(content);
            return docId;
        }

        /// <summary>
        /// Get a unique id for document persist
        /// </summary>
        /// <returns> unique id in format {Day}{Hour}{Minute}{Second}{Millisecond}{random number}</returns>
        private string GetUniqueDocumentId()
        {
            Random rand = new Random();
            string suffix = rand.Next().ToString();
            DateTime n = DateTime.Now;

            return $"{n.Day}{n.Hour}{n.Minute}{n.Second}{n.Millisecond}{suffix}";
        }
    }
}
