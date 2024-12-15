using BlazorFrontend.Models;
using Serilog;

namespace BlazorFrontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            // Wyślij żądanie do IdentityApi, aby uzyskać token
            /*var response = await _httpClient.PostAsJsonAsync("connect/token", loginRequest);*/
            var response = await _httpClient.PostAsJsonAsync("api/Users/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to authenticate.");
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return authResponse?.Token ?? throw new Exception("Token not found.");
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new { username = username, password = password };

                var response = await _httpClient.PostAsJsonAsync("api/Users/login", loginRequest);

                // Dodaj debugowanie odpowiedzi
                var responseContent = await response.Content.ReadAsStringAsync();
                Log.Information($"Response Status Code: {response.StatusCode}");
                Log.Information($"Response Body: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error("Failed to authenticate.");
                    return false;
                }

                var content = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                var token = content["token"];

                // Ustawienie tokena (opcjonalne)
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                return true; // Logowanie powiodło się
            }
            catch (Exception ex)
            {
                Log.Error($"Exception during token retrieval: {ex.Message}");
                return false; // Logowanie nie powiodło się
            }
        }
    }
}
