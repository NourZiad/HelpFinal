using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class Menu : CommonProp
    {
        [Display(Name ="Menu Id")]
        public int MenuId { get; set; }
        [Display(Name = "Menu Title")]
        [Required(ErrorMessage ="Enter Title")]
        [MaxLength(13,ErrorMessage ="Max 13 Char")]
        [MinLength(3,ErrorMessage ="Min 3 Char")]
        public string? Title { get; set; }
        [Display(Name = "Menu Title Url")]
        [Required(ErrorMessage = "Enter Title Url")]
        public string? UrlTitle { get; set; }
    }
}
