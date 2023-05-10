using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models.ViewModels
{
    public class TestimonialViewModel :CommonProp
    {
        [Display(Name = "Enter Id ")]
        public int TestimonialId { get; set; }
        [Required(ErrorMessage = "Enter Name ")]
        [Display(Name = "Enter Name ")]
        public string? TestimonialName { get; set; }
        [Required(ErrorMessage = "Enter University")]
        [Display(Name = "Enter University ")]
        public string? Collage { get; set; }
        [Required(ErrorMessage = "Enter Description Or Opinion ")]
        [Display(Name = "Enter Description Or Opinion ")]
        public string? TestimonialDesc { get; set; }
        [Display(Name = "Select Image")]

        public IFormFile? Image { get; set; }
    }
}
