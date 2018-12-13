using JotFinalProject.Data;
using JotFinalProject.Models.Interfaces;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JotFinalProject.Models.Services
{
    public class CognitiveService : ICognitive
    {
        public static string ApiKey { get; set; }
        private readonly IImageUploaded _imageUpload;
        
        public CognitiveService(IImageUploaded imageUpload)
        {
            _imageUpload = imageUpload;
        }

        public async Task<ImageUploaded> AnalyzeImage(string imageUrl, string userID, int categoryID, string fileName)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);

            // Request parameters
            var uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/recognizeText?mode=Printed";
            
            StringContent content = generateBody(imageUrl);
            HttpResponseMessage response = await client.PostAsync(uri, content);
            
            return await SaveUploadedImage(userID, imageUrl, categoryID, fileName, response.Headers.GetValues("Operation-Location").FirstOrDefault());
        }

        private async Task<ImageUploaded> SaveUploadedImage(string userID, string imageUrl, int categoryID, string fileName, string operationLocation)
        {
            var imageUploaded = new ImageUploaded()
            {
                UserId = userID,
                ImageUrl = imageUrl,
                OperationLocation = operationLocation,
                Note = new Note { UserID = userID , CategoryID = categoryID, Title = fileName }
            };

            await _imageUpload.CreateImageUploaded(imageUploaded);
            return imageUploaded;
        }

        private StringContent generateBody(string imageUrl)
        {
            var body = new { url = imageUrl };
            return new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
        }

        public async Task<ApiResults> GetContentFromOperationLocation(ImageUploaded imageUploaded)
        {
            var operationLocation = imageUploaded.OperationLocation;
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
            ApiResults apiReponseBody = await GetResult(client, operationLocation);

            return apiReponseBody;
        }

        private async Task<ApiResults> GetResult(HttpClient client, string operationLocation)
        {
            // api call every 5 second until result is either a fail or success
            while (true)
            {
                HttpResponseMessage response = await client.GetAsync(operationLocation);
                response.EnsureSuccessStatusCode();

                ApiResults apiReponseBody = JsonConvert.DeserializeObject<ApiResults>(await response.Content.ReadAsStringAsync());
                if (apiReponseBody.Status == "Failed" || apiReponseBody.Status == "Succeeded")
                {
                    return apiReponseBody;
                }
                await Task.Delay(5000);
            }
        }

    }
}
