using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models
{
    public class Blob
    {
        public CloudStorageAccount CloudStorageAccount { get; set; }
        public CloudBlobClient CloudBlobClient { get; set; }

        public Blob(string storageAccountName, string accessKey)
        {
            CloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=jotfinal;AccountKey=SxCfWKZY9pkQ2gU5yx0iZAm4WgyTqhseMWOia6B+0it+Jn6RhAGDRTT26PYJRYnIWEFE9I6b7QwJj2AL5Vxqtg==;EndpointSuffix=core.windows.net");

            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
        }

        public async Task<CloudBlobContainer> GetContainer(string containerName)
        {
            CloudBlobContainer cbc = CloudBlobClient.GetContainerReference(containerName);

            await cbc.CreateIfNotExistsAsync();

            await cbc.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            return cbc;
        }

        public CloudBlob GetBlob(string imageName, string containerName)
        {
            var container = CloudBlobClient.GetContainerReference(containerName);
            var blob = container.GetBlobReference(imageName);

            return blob;
        }

        public async void UploadFile(CloudBlobContainer cloudBlobContainer, string fileName, string filePath)
        {
            var blobFile = cloudBlobContainer.GetBlockBlobReference(fileName);

            await blobFile.UploadFromFileAsync(filePath);
        }
    }
}
