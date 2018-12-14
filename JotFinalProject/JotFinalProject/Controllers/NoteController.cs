using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        INote _note;
        ICategory _category;

        UserManager<ApplicationUser> _userManager;

        public NoteController(IConfiguration configuration, IHostingEnvironment environment, IImageUploaded imageUploaded, ICognitive cognitive, UserManager<ApplicationUser> userManager, INote note, ICategory category)
        {
            _configuration = configuration;
            _environment = environment;
            _imageUploaded = imageUploaded;
            _cognitive = cognitive;
            _userManager = userManager;
            _note = note;
            _category = category;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload()
        {
            ViewBag.Categories = await _category.GetCategories();
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

            return filePath;
        }

        [HttpPost]
        public async Task<IActionResult> TestBlob(IFormFile file, string fileName, string categoryID)
        {
            var filePath = await Post(file);

            Blob blob = new Blob(_configuration["BlobStorageAccountName"], _configuration["BlobStorageKey"]);

            var mycontainer = await blob.GetContainer("jotnotes");

            await blob.UploadFile(mycontainer, fileName, filePath);


            // grabbing image from blob storage
            var image = blob.GetBlob(fileName, "jotnotes");

            //grabbing url from image for use in API
            string imageUrl = image.StorageUri.PrimaryUri.ToString();


            var user = await _userManager.GetUserAsync(User);

            //making new imageUploaded from API call
            var newImage = await _cognitive.AnalyzeImage(imageUrl, user.Id, Convert.ToInt32(categoryID), fileName);

            return RedirectToAction("Details", "Note", new { id = newImage.Id });
        }


        public async Task<IActionResult> Details(int id)
        {
            var imageUploaded = await _imageUploaded.GetImageUploaded(id);
            if (imageUploaded == null)
            {
                return NotFound();
            }

            if (imageUploaded.Note.Text == null)
            {
                await GenereateNoteText(imageUploaded);
            }
            ViewBag.ImgUrl = imageUploaded.ImageUrl;
            var note = await _note.GetNote(imageUploaded.Note.ID);
            return View(note);
        }

        private async Task GenereateNoteText(ImageUploaded imageUploaded)
        {
            ApiResults apiReponseBody = await _cognitive.GetContentFromOperationLocation(imageUploaded);

            imageUploaded.Note.Text = BuildNoteText(apiReponseBody);
            await _note.UpdateNote(imageUploaded.Note);
        }

        private string BuildNoteText(ApiResults apiReponseBody)
        {
            StringBuilder output = new StringBuilder();
            foreach (var item in apiReponseBody.RecognitionResult.Lines)
            {
                output.Append(item.Text);
                output.Append(Environment.NewLine);
            }
            return output.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> Details(Note note, string imgUrl)
        {
            ViewBag.ImgUrl = imgUrl;
            await _note.UpdateNote(note);
            return View(note);
        }
    }
}