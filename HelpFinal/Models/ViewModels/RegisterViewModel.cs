﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models.ViewModels
{
    public class RegisterViewModel 
    {


        public string? Id { get; set; }

        [Display(Name = "Enter Your ID")]
        public string? StudentId { get; set; }

        [Required]
        [Display(Name = "Enter Your Name")]
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
        
        public bool IsVolunteer { get; set; }
        public bool IsDisabled { get; set; }
        public string? Skills { get; set; }
        public string? DisabilityType { get; set; }
        

        //[Required]
        //[Display(Name = "Confirm Your Password")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password is not Match!")]
        //public string ConfirmPassword { get; set; }

        //// Additional properties specific to user registration

        //// Navigation properties for related entities
        //public virtual VolunteerViewModel VolunteerViewModel { get; set; }
        //public virtual DisabledViewModel DisabledViewModel { get; set; }
    }
}
