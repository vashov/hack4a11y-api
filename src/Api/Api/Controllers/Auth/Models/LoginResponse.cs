﻿namespace Api.Controllers.Auth.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public long UserId { get; set; }
    }
}
