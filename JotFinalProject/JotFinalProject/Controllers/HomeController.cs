using System;
using System.Text;
using System.Threading.Tasks;
using JotFinalProject.Models;
using JotFinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JotFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageUploaded _imageUploaded;
        private readonly ICognitive _cognitive;
        private readonly INote _note;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IImageUploaded imageUploaded, ICognitive cognitive, INote note, UserManager<ApplicationUser> userManager)
        {
            _imageUploaded = imageUploaded;
            _cognitive = cognitive;
            _note = note;
            _userManager = userManager;
            _note = note;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User);
            string ID = user.Id.ToString();

            var imageUploadeds = _imageUploaded.GetImageUploadeds(ID);
            return View(imageUploadeds);
        }

    }
}