using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class Contact : CommonProp
    {
        public int ContactId { get; set; }
        [Required]
        [Display(Name="Enter Name")]
        public string? Name { get; set; }
        [Display(Name = "Enter Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }
        public string? Subject { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Message { get; set; }


    }
}
