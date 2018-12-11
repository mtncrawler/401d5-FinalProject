using Newtonsoft.Json;
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

    public partial class Welcome
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("recognitionResult")]
        public RecognitionResult RecognitionResult { get; set; }
    }

    public partial class RecognitionResult
    {
        [JsonProperty("lines")]
        public Line[] Lines { get; set; }
    }

    public partial class Line
    {
        [JsonProperty("boundingBox")]
        public long[] BoundingBox { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("words")]
        public Word[] Words { get; set; }
    }

    public partial class Word
    {
        [JsonProperty("boundingBox")]
        public long[] BoundingBox { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
