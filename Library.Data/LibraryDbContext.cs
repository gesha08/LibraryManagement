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
        }
    }
}