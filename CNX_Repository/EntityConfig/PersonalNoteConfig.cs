using CNX_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNX_Repository.EntityConfig
{
    public class PersonalNoteConfig : IEntityTypeConfiguration<PersonalNote>
    {
        public void Configure(EntityTypeBuilder<PersonalNote> builder)
        {
            builder.HasKey(personalNote => personalNote.Id);

            builder.Property(personalNote => personalNote.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(personalNote => personalNote.UserId)
                .IsRequired();

            builder.Property(personalNote => personalNote.Note)
                .IsRequired()
                .HasMaxLength(140);

            builder.HasOne(personalNote => personalNote.User)
                .WithMany(user => user.PersonalNoteList)
                .HasForeignKey(personalNote => personalNote.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(personalNote => personalNote.CascadeMode);
            builder.Ignore(personalNote => personalNote.ValidationResult);
        }
    }
}
