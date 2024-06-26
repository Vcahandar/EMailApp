﻿using System.ComponentModel.DataAnnotations;

public class RegisterVM
{
    [Required(ErrorMessage = "Please enter your name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter your surname")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter your username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Please enter your password")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords are not compatible")]
    public string ConfirmPassword { get; set; }

    // [Required(ErrorMessage = "The ImageUrl field is required.")]
    public string? ImageUrl { get; set; }
}
