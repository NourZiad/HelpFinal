using HelpFinal.Models.SharedProp;

namespace HelpFinal.Models
{
    public class Request : CommonProp
    {
        public int RequestId { get; set; }
        
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? Uni { get; set; }
        public string? TxtLink { get; set; }
        public string? TxtLink2 { get; set; }
    }
}
