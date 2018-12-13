using System.Threading.Tasks;

namespace JotFinalProject.Models.Interfaces
{
    public interface ICognitive
    {
        Task<ImageUploaded> AnalyzeImage(string imageUrl, string userID);

        Task<ApiResults> GetContentFromOperationLocation(ImageUploaded imageUploaded);
    }
}
