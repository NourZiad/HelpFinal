using HelpFinal.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace HelpFinal.Models
{
    public class Fact : CommonProp
    {
        public int FactId { get; set; }
        [Display(Name ="Enter Name")]
        public string? Name { get; set; }
        [Display(Name = "Enter Number")]

        public string? Number { get; set; }
    }
}
