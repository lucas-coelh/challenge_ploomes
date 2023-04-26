using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<AplicationUser>
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<AplicationUser> AplicationUser { get; set; }
        public DbSet<Message> Message { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AplicationUser>().ToTable("AspNetUsers").HasKey(t=> t.Id);

            base.OnModelCreating(builder);
        }

        public string GetConnectionString()
        {
            return Database.GetDbConnection().ConnectionString;
        }

    }
}
