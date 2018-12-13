using System;
using System.Text;
using System.Threading.Tasks;
using JotFinalProject.Models;
using JotFinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JotFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageUploaded _imageUploaded;
        private readonly ICognitive _cognitive;
        private readonly INote _note;

        public HomeController(IImageUploaded imageUploaded, ICognitive cognitive, INote note)
        {
            _imageUploaded = imageUploaded;
            _cognitive = cognitive;
            _note = note;
        }

        public async Task<IActionResult> Index()
        {
            var imageUploaded = await _cognitive.AnalyzeImage("http://2.bp.blogspot.com/-jCjNdPcve0U/UbYj7sapwCI/AAAAAAAABY4/IFI2Ix5MezA/s1600/IMG_0127.JPG", "1");
            //await _imageUploaded.CreateImageUploaded(imageUploaded);
            var imageUploadeds = _imageUploaded.GetImageUploadeds("1");
            return View(imageUploadeds);
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
                //var apiReponseBody = await _cognitive.GetContentFromOperationLocation(imageUploaded);
                StringBuilder output = new StringBuilder();
                ApiResults apiReponseBody = await _cognitive.GetContentFromOperationLocation(imageUploaded);

                if (apiReponseBody != null)
                {
                    foreach (var item in apiReponseBody.RecognitionResult.Lines)
                    {
                        output.Append(item.Text);
                        output.Append(Environment.NewLine);
                    }
                    imageUploaded.Note.Text = output.ToString();
                    await _note.UpdateNote(imageUploaded.Note);
                }

            }

            var note = await _note.GetNote(imageUploaded.Note.ID);
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Note note)
        {
            await _note.UpdateNote(note);
            return View(note);
        }
    }
}