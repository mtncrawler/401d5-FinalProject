using Newtonsoft.Json;
using System;
using System.Collections;
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

    public partial class ApiResults
    { 
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("recognitionResult")]
        public ApiRecognitionResult RecognitionResult { get; set; }
    }

    public partial class ApiRecognitionResult
    {
        [JsonProperty("lines")]
        public ApiLine[] Lines { get; set; }

    }

    public partial class ApiLine
    {
        [JsonProperty("boundingBox")]
        public long[] BoundingBox { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("words")]
        public ApiWord[] Words { get; set; }
    }

    public partial class ApiWord
    {
        [JsonProperty("boundingBox")]
        public long[] BoundingBox { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
