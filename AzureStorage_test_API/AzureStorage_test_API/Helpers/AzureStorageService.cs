using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorage_test_API.Helpers
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string connectionString;
        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnectionString");
        }
        public async Task DeleteFile(string containerName, string fileRoute)
        {
            var client = new BlobContainerClient(connectionString, containerName);
            
            if (!await client.ExistsAsync()) return;

            string fileName = Path.GetFileName(fileRoute);

            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(string containerName, string oldFileRoute, IFormFile newFile)
        {
            await DeleteFile(containerName, oldFileRoute);
            return await UploadFile(containerName, newFile);
        }

        public async Task<string> UploadFile(string containerName, IFormFile file)
        {
            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // custom file name

            var blob = client.GetBlobClient(file.FileName);
            await blob.UploadAsync(file.OpenReadStream());

            return blob.Uri.ToString(); 
        }
    }
}
