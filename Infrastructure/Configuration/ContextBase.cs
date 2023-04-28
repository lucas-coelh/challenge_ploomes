using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Message> Message { get; set; }
        public DbSet<ApplicationUser> AplicationUser { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t=> t.Id);

            base.OnModelCreating(builder);
        }

        public string GetConnectionString()
        {
            return "Server=tcp:ploomes-db-server.database.windows.net,1433;Initial Catalog=ploomes-db;Persist Security Info=False;User ID=ploomesAPI;Password=h418M2bq^APp;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        }

    }
}
