using AzureStorage_test_API.Helpers;
using AzureStorage_test_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorage_test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IFileStorageService storageService;
        private readonly string containerName = "albums";

        public AlbumController(IFileStorageService storageService)
        {
            this.storageService = storageService;
        }

        [HttpGet(":id")]
        public ActionResult<Album> Get(int id)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromForm]CreateAlbumDTO album)
        {
            if (album.Cover != null)
            {
                string url = await storageService.UploadFile(containerName, album.Cover);
                return Ok(url);
            }

            return BadRequest();
        }
    }
}
