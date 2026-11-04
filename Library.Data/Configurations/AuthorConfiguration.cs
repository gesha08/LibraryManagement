using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Biography)
                .HasMaxLength(500);

            builder.HasData(
                new Author
                {
                    Id = 101,
                    FirstName = "George",
                    LastName = "Orwell",
                    Biography = "English novelist, essayist, journalist and critic.",
                    BirthDate = new DateTime(1903, 6, 25)
                },
                new Author
                {
                    Id = 102,
                    FirstName = "Jane",
                    LastName = "Austen",
                    Biography = "English novelist known primarily for her six major novels.",
                    BirthDate = new DateTime(1775, 12, 16)
                },
                new Author
                {
                    Id = 103,
                    FirstName = "Ray",
                    LastName = "Bradbury",
                    Biography = "American author and screenwriter.",
                    BirthDate = new DateTime(1920, 8, 22)
                }
            );
        }
    }
}
