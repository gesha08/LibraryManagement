using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Library.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<BookReader> BookReaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookReader>()
                .HasKey(br => new { br.BookId, br.ReaderId });

            modelBuilder.Entity<BookReader>()
                .HasOne(br => br.Book)
                .WithMany(b => b.BookReaders)
                .HasForeignKey(br => br.BookId);

            modelBuilder.Entity<BookReader>()
                .HasOne(br => br.Reader)
                .WithMany(r => r.BookReaders)
                .HasForeignKey(br => br.ReaderId);

            // Seed data
            modelBuilder.Entity<Author>().HasData(
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

            modelBuilder.Entity<Book>().HasData(
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

            modelBuilder.Entity<Reader>().HasData(
                new Reader
                {
                    Id = 101,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "ivan.petrov@example.com",
                    PhoneNumber = "+359888123456",
                    RegistrationDate = new DateTime(2024, 1, 15)
                },
                new Reader
                {
                    Id = 102,
                    FirstName = "Maria",
                    LastName = "Ivanova",
                    Email = "maria.ivanova@example.com",
                    PhoneNumber = "+359888987654",
                    RegistrationDate = new DateTime(2024, 3, 20)
                },
                new Reader
                {
                    Id = 103,
                    FirstName = "Georgi",
                    LastName = "Dimitrov",
                    Email = "georgi.dimitrov@example.com",
                    PhoneNumber = "+359887111222",
                    RegistrationDate = new DateTime(2024, 6, 10)
                }
            );

            modelBuilder.Entity<BookReader>().HasData(
                new { BookId = 101, ReaderId = 101, BorrowedDate = new DateTime(2024, 2, 1), ReturnedDate = (DateTime?)new DateTime(2024, 2, 15) },
                new { BookId = 102, ReaderId = 101, BorrowedDate = new DateTime(2024, 3, 22), ReturnedDate = (DateTime?)null },
                new { BookId = 103, ReaderId = 102, BorrowedDate = new DateTime(2024, 4, 5), ReturnedDate = (DateTime?)new DateTime(2024, 4, 20) },
                new { BookId = 104, ReaderId = 103, BorrowedDate = new DateTime(2024, 7, 1), ReturnedDate = (DateTime?)null },
                new { BookId = 101, ReaderId = 103, BorrowedDate = new DateTime(2024, 8, 10), ReturnedDate = (DateTime?)null }
            );
        }
    }
}