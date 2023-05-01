using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models.ViewModels
{
    public class LoginViewModel
    {
        
        [Required]
        [Display(Name = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
       
        [Required]
        [Display(Name = "Enter Your Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
       
    }
}
