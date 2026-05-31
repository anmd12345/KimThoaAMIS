using System;
using ManagementKimThoa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementKimThoa.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NoteTitle)
                   .HasMaxLength(500);

            builder.Property(x => x.NoteDescription)
                   .HasMaxLength(2000);

            builder.Property(x => x.DateCreateNote)
                   .HasMaxLength(100)
                   .IsUnicode(false);
        }
    }
}

