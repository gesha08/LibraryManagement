using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .HasMaxLength(200);

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(13);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            builder.HasData(
                new Book
                {
                    Id = 101,
                    Title = "1984",
                    Description = "A dystopian social science fiction novel.",
                    ISBN = "9780451524935",
                    Pages = 328,
                    PublicationYear = 1949,
                    AuthorId = 101
                },
                new Book
                {
                    Id = 102,
                    Title = "Pride and Prejudice",
                    Description = "A romantic novel of manners.",
                    ISBN = "9781503290563",
                    Pages = 432,
                    PublicationYear = 1813,
                    AuthorId = 102
                },
                new Book
                {
                    Id = 103,
                    Title = "Fahrenheit 451",
                    Description = "A dystopian novel about censorship.",
                    ISBN = "9781451673319",
                    Pages = 249,
                    PublicationYear = 1953,
                    AuthorId = 103
                },
                new Book
                {
                    Id = 104,
                    Title = "Animal Farm",
                    Description = "A satirical allegorical novella.",
                    ISBN = "9780451526342",
                    Pages = 144,
                    PublicationYear = 1945,
                    AuthorId = 101
                }
            );
        }
    }
}
