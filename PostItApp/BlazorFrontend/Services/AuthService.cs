using Blazored.LocalStorage;
using BlazorFrontend.Models;
using Serilog;

namespace BlazorFrontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
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

        public async Task<bool> LoginAsync(string email, string password)
        {
            
            var loginRequest = new { email = email, password = password };

            var response = await _httpClient.PostAsJsonAsync("api/Users/login", loginRequest);


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                var token = content["token"];
                await _localStorage.SetItemAsync("authToken", token);
                return true;
            }

            return false;


        }
        public async Task<bool> RegisterUserAsync(string email, string firstName, string lastName, string password)
        {
            try
            {
                var registerRequest = new RegisterModel
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync("api/Users/register", registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Log.Error("Registration failed: {0}", response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception during registration: {0}", ex.Message);
                return false;
            }
        }
    }
}
