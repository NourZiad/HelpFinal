﻿using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? StudentId { get; set; }
        [Required]
        [Display(Name ="Enter Your Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Enter Your Phone")]
        public string? Phone { get; set; }
        [Required]
        [Display(Name = "Enter Your Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Display(Name = "Confirm Your Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password is not Match!")]
        public string? ConfirmPassword { get; set; }
    }
}
