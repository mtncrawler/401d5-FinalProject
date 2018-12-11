using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models
{
    public class ImageUploaded
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string OperationLocation { get; set; }

        public string ImageUrl { get; set; }
    }
}
