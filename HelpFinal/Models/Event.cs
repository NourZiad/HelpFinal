using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class Event : CommonProp
    {
        [Display(Name = "Enter Id")]
        public int EventId { get; set; }
        [Required(ErrorMessage = "Enter Event Title ")]
        [Display(Name ="Enter Title ")]
        public string? EventTitle { get; set; }
        [Required(ErrorMessage = "Enter Event Description ")]
        [Display(Name = "Enter Description ")]
        public string? EventDesc { get; set; }
        public string? EventImg { get; set; }
        [Required(ErrorMessage = "Enter Event Location ")]
        [Display(Name = "Enter Location ")]
        public string? EventLocation { get; set; }
        public string? TxtLink { get; set; }

        public string? UrlLink { get; set; }
        [Required(ErrorMessage = "Enter  Date Event")]

        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Enter Time Event")]

        public DateTime? Time { get; set; }
    }
}
