using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorage_test_API.Helpers
{
    public interface IFileStorageService
    {
        Task DeleteFile(string containerName, string fileRoute);
        Task<string> UploadFile(string containerName, IFormFile file);
        Task<string> EditFile(string containerName, string oldFileRoute, IFormFile newFile);
    }
}
