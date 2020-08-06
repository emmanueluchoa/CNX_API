using CNX_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNX_Repository.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(user => user.UserName)
                .IsRequired();

            builder.Property(user => user.UserPassword)
                .IsRequired();

            builder.Property(user => user.UserHomeTown)
              .IsRequired();

            builder.Property(user => user.UserEmail)
              .IsRequired();

            builder.HasIndex(user => user.UserName)
                .IsUnique();

            builder.HasIndex(user => user.UserEmail)
                .IsUnique();

            builder.Ignore(personalNote => personalNote.CascadeMode);
            builder.Ignore(personalNote => personalNote.ValidationResult);
        }
    }
}
