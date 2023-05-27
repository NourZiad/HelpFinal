using HelpFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpFinal.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HelpFinal.Data
{
    public class FinalDbContext : IdentityDbContext
    {
        public FinalDbContext(DbContextOptions<FinalDbContext> options):base(options)
        {
            
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Fact> Facts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<StdDisbled> StdDisbleds { get; set; }
        public DbSet<HelpFinal.Models.ViewModels.VolunteerViewModel> VolunteerViewModel { get; set; } = default!;

	



		//public DbSet<VolunteerRegistrationViewModel> VolunteerRegistrationViewModels { get; set; }
		//public DbSet<NeedHelpRegistrationViewModel> NeedHelpRegistrationViewModels { get; set; }
		//public DbSet<HelpFinal.Models.ViewModels.VolunteerRegistrationViewModel> VolunteerRegistrationViewModel { get; set; } = default!;
		//public DbSet<HelpFinal.Models.ViewModels.NeedHelpRegistrationViewModel> NeedHelpRegistrationViewModel { get; set; } = default!;
	}
}
