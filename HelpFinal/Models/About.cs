using HelpFinal.Models.SharedProp;

namespace HelpFinal.Models
{
    public class About : CommonProp
    {
        public int AboutId { get; set; }

        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? AboutUs { get; set; }
        public string? Mision { get; set; }
        public string? Vision { get; set; }
        public string? Image { get; set; }
    }
}
