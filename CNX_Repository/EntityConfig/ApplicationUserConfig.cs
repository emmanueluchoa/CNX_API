using CNX_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNX_Repository.EntityConfig
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.ToTable("Users");

            builder.HasKey(personalNote => personalNote.Id);

            builder.Property(personalNote => personalNote.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(user => user.Email)
                .IsRequired();

            builder.Property(user => user.UserName)
               .IsRequired();

            builder.Property(user => user.PasswordHash)
                .IsRequired();

            builder.Property(user => user.Locale)
               .IsRequired();

            builder.HasIndex(user => user.UserName)
                .IsUnique();

            builder.HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
