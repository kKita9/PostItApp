using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorFrontend.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

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

        public async Task<List<Friend>> GetFriendsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
             return await _httpClient.GetFromJsonAsync<List<Friend>>("api/Friends");
            
        }
    }
}
