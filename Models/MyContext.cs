using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        // Your DB Sets go HERE!
        public DbSet<MainUser> Users {get; set;}
        public DbSet<Wedding> Weddings {get; set;}
        public DbSet<Attending> Attendees {get; set;}

    }

}
