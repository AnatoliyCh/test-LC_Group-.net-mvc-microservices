using System.Text.Json.Serialization;

namespace PoemsModule.Models
{
    public class Poem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}