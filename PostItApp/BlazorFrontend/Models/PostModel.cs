using System.Text.Json.Serialization;

namespace BlazorFrontend.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [JsonPropertyName("authorName")]
        public string UserName { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("likeCount")]
        public int Likes { get; set; }

        [JsonPropertyName("isLikedByCurrentUser")]
        public bool IsLikedByCurrentUser { get; set; }
    }
}
