using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JotFinalProject.Controllers
{
    public class NoteController : Controller
    {
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
    }
}