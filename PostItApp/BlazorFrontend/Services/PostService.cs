using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorFrontend.Models;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace BlazorFrontend.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public PostService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<List<PostModel>> GetUserAndFriendsPostsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.GetFromJsonAsync<List<PostModel>>("api/Post/user-and-friends-posts");
        }

        public async Task<HttpResponseMessage> ToggleLikePostAsync(int postId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.PostAsync($"api/Post/toggle-like/{postId}", null);
        }

        public async Task<HttpResponseMessage> AddPostAsync(PostModel newPost)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.PostAsJsonAsync("api/Post/add", newPost);
        }
    }

}