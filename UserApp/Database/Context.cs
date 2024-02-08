using Microsoft.EntityFrameworkCore;
using UserApp.Database.Model;

namespace UserApp.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
    
        }

        public DbSet<LinkUser> LinkUser { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-JJ3E769; initial catalog=UserApplication;integrated security=true; User ID=sa;Password=Development@101");
        //}

    }
}
