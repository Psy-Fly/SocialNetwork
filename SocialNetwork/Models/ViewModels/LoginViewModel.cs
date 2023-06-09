﻿using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    [Required]
    [Display(Name = "Запомнить")]
    public bool RememberMe { get; set; }
    
    public string ReturnUrl { get; set; }
    
}