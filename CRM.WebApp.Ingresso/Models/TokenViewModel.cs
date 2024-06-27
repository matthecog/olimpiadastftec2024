﻿using Newtonsoft.Json.Linq;

namespace CRM.WebApp.Ingresso.Models
{
    public class TokenViewModel
    {
        public string accessToken { get; set; }
        public string? refreshToken { get; set; }
        public DateTime expiration { get; set; }
        public UserInfoViewModel userInfo { get; set; } 
    }
}
