using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models.ViewModels
{
    public class StdDisbledViewModel
    {
        public int Id { get; set; }
        public IFormFile? Img { get; set; }
        public string? AssistanceNeeded { get; set; }
        public string? Place { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Enter Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm }", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        public string? Description { get; set; }

        public string? AcceptedBy { get; set; }

        public string? UserId { get; set; }
    }
}
