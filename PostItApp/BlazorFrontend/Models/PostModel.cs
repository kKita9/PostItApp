using System.Text.Json.Serialization;

namespace BlazorFrontend.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "Karol";  // todo change later  

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        public int Likes { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;  // todo change to list of comments 
    }
}
