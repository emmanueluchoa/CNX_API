using CNX_Domain.Entities;
using CNX_Repository.EntityConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CNX_Repository.Context
{
    public class CnxContext : IdentityDbContext<User>
    {
        public CnxContext(DbContextOptions<CnxContext> options) : base(options) { }
        public override DbSet<User> Users { get; set; }
        public DbSet<PersonalNote> PersonalNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
            modelBuilder.ApplyConfiguration(new PersonalNoteConfig());
        }
    }
}
