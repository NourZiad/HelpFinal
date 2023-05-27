using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class StdDisbled
    {
            public int Id { get; set; }

            [Required]
            public string? Name { get; set; }
                         
            [Required]   
            public string? StudentID { get; set; }
                         
              
            public string? DisabilityType { get; set; }
                         
            public string? AssistanceNeeded { get; set; }
            public string? Place { get; set; }

        [Required(ErrorMessage = "Enter Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Enter Event Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm }", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Phone]
            public string? PhoneNumber { get; set; }
        public string? AcceptedBy { get; set; }

        public string? UserId { get; set; }
        //public ApplicationUser User { get; set; }
    }

    }

