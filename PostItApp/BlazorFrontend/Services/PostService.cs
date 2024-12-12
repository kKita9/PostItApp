using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorFrontend.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>("api/Post");
        }

        public async Task CreatePostAsync(Post post)
        {
            await _httpClient.PostAsJsonAsync("api/Post", post);
        }
    }
}
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}