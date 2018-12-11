using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models.Interfaces
{
    public interface IImageUploaded
    {
        
        //Create
        Task CreateImageUploaded(ImageUploaded imageUploaded);

        //Read
        List<ImageUploaded> GetImageUploadeds(string userId);

        //Get One Object
        ImageUploaded GetImageUploaded(int id);
    }
}
