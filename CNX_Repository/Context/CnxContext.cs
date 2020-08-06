using CNX_Domain.Entities;
using CNX_Repository.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace CNX_Repository.Context
{
    public class CnxContext : DbContext
    {
        public CnxContext(DbContextOptions<CnxContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<PersonalNote> PersonalNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new PersonalNoteConfig());
        }

    }
}
