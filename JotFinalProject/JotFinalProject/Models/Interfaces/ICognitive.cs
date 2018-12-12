using System.Threading.Tasks;

namespace JotFinalProject.Models.Interfaces
{
    public interface ICognitive
    {
        Task<ImageUploaded> AnalyzeImage();

        Task<ApiResults> GetContentFromOperationLocation(ImageUploaded imageUploaded);
    }
}
