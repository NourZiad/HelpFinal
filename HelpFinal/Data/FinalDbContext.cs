using HelpFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<University> Universities { get; set; }
    }
}
