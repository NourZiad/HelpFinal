using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class University : CommonProp
    {
        [Display(Name = "Enter University Id")]
        public int UniversityId { get; set; }
        [Display(Name ="Enter University Name")]
        [Required(ErrorMessage ="Enter University")]
        public string? UniName { get; set; }
        [Display(Name = "Enter Description")]
        [Required(ErrorMessage = "Enter Description")]
        public string? Description { get; set; }
    }
}
