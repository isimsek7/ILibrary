using System;
using System.ComponentModel.DataAnnotations;

namespace BookInfoApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }  // User's email address

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // User's password
    }
}


