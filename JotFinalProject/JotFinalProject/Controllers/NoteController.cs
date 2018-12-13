using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JotFinalProject.Models;
using JotFinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JotFinalProject.Controllers
{
    public class NoteController : Controller
    {
        IConfiguration _configuration;
        IHostingEnvironment _environment;
        IImageUploaded _imageUploaded;
        ICognitive _cognitive;
        UserManager<ApplicationUser> _userManager;

        public NoteController(IConfiguration configuration, IHostingEnvironment environment, IImageUploaded imageUploaded, ICognitive cognitive, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _environment = environment;
            _imageUploaded = imageUploaded;
            _cognitive = cognitive;
            _userManager = userManager;
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
        public async Task<string> Post(IFormFile file)
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

            //return Ok(new { count = 1, size, filePath });

            return filePath;
        }

        [HttpPost]
        public async Task<IActionResult> TestBlob(IFormFile file, string fileName)
        {
            var filePath = await Post(file);

            Blob blob = new Blob(_configuration["BlobStorageAccountName"], _configuration["BlobStorageKey"]);

            var mycontainer = await blob.GetContainer("jotnotes");
            
            
            //string filepath = $"{_environment.WebRootPath}\\Images\\testImage.jpg";

            await blob.UploadFile(mycontainer, fileName, filePath);


            // grabbing image from blob storage
            var image = blob.GetBlob(fileName, "jotnotes");

            //grabbing url from image for use in API
            string imageUrl = image.StorageUri.PrimaryUri.ToString();

            // relates to line 92 ... 1 is hardcoded for now
            // TODO: Uncomment
           var user = await _userManager.GetUserAsync(User);

            //making new imageUploaded from API call
            var newImage = await _cognitive.AnalyzeImage(imageUrl, "1");

            return RedirectToAction("Details", "Home" , new { id = newImage.Id });
        }

    }
}