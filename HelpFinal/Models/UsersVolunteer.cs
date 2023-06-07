using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class UsersVolunteer
    {
        public string? Id { get; set; }
        public string? StudentId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Skills { get; set; }
    }

}
