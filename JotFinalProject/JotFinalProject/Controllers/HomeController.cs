using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JotFinalProject.Models;
using JotFinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JotFinalProject.Controllers
{
    public class HomeController : Controller
    {
        public static string apiKey { get; set; }

        private readonly IImageUploaded _imageUploaded;

        public HomeController(IImageUploaded imageUploaded)
        {
            _imageUploaded = imageUploaded;
        }

        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

            // Request parameters
            var uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/recognizeText?mode=Printed";

            HttpResponseMessage response;
            var imageUrl = "http://2.bp.blogspot.com/-jCjNdPcve0U/UbYj7sapwCI/AAAAAAAABY4/IFI2Ix5MezA/s1600/IMG_0127.JPG";
            var body = new { url = imageUrl };

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);

            var imageUploaded = new ImageUploaded()
            {
                UserId = "1",
                ImageUrl = imageUrl,
                OperationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault()
            };
            await _imageUploaded.CreateImageUploaded(imageUploaded);
            var imageUploadeds = _imageUploaded.GetImageUploaded("1");
            return View(imageUploadeds);
        }
    }
}