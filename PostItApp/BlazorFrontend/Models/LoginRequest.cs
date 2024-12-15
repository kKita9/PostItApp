﻿using System.Text.Json.Serialization;

namespace BlazorFrontend.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
     
    }
}
