using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models.ViewModels
{
    public class SliderViewModel : CommonProp
    {
        [Display(Name = "Slider Id")]
        public int SliderId { get; set; }
        [Display(Name = "Slider Title")]
        [Required(ErrorMessage = "Enter Title")]
        public string? SliderTitle { get; set; }
        [Display(Name = "Slider Sub Title")]
        [Required(ErrorMessage = "Enter Sub Title")]

        public string? SliderSubTitle { get; set; }

        public string? TxtLink { get; set; }
        public string? UrlLink { get; set; }
        [Required(ErrorMessage = "Select Image")]
        public IFormFile? SliderImg { get; set; }
    }
}
