using System;
using System.ComponentModel.DataAnnotations;

namespace BookInfoApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }  // User's full name

        [Required]
        [EmailAddress]
        public string Email { get; set; }  // User's email address

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // User's password

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }  // Confirm password field

        [Phone]
        public string PhoneNumber { get; set; }  // User's phone number
    }
}


