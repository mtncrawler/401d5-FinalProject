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

        public IActionResult Index()
        {
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
        public async Task<IActionResult> Details(Note note)
        {
            await _note.UpdateNote(note);
            return View(note);
        }
    }
}