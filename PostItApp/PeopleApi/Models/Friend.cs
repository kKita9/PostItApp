﻿using System.Text.Json.Serialization;


namespace PeopleApi.Models
{
    public class Friend
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
