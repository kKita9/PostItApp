using System.Net.Http.Json;
using Blazored.LocalStorage;
using BlazorFrontend.Models;

namespace BlazorFrontend.Services
{
    public class FriendService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public FriendService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<List<FriendModel>> GetFriendsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await _httpClient.GetFromJsonAsync<List<FriendModel>>("api/Friends/list")
                   ?? new List<FriendModel>();
        }
    }
}
