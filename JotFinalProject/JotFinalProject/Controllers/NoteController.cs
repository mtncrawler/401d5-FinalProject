using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JotFinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JotFinalProject.Controllers
{
    public class NoteController : Controller
    {
        IConfiguration _configuration;
        IHostingEnvironment _environment;

        public NoteController(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;

            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = 1, size, filePath });
        }

        public async void TestBlob()
        {
            Blob blob = new Blob(_configuration["BlobStorageAccountName"], _configuration["BlobStorageKey"]);

            var mycontainer = await blob.GetContainer("jotnotes");

            //var image = blob.GetBlob("AboutMe.PNG", "jotnotes");

            //string imageURL = image.Uri.ToString();

            string filepath = $"{_environment.WebRootPath}\\Images\\testImage.jpg";

            blob.UploadFile(mycontainer, "test1", filepath);
        }
    }
}