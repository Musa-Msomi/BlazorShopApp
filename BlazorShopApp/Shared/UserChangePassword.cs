﻿using System.ComponentModel.DataAnnotations;

namespace BlazorShopApp.Shared
{
    public class UserChangePassword
    {
        [Required, StringLength(100, MinimumLength =6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}