using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configurations
{
    public class BookReaderConfiguration : IEntityTypeConfiguration<BookReader>
    {
        public void Configure(EntityTypeBuilder<BookReader> builder)
        {
            builder.HasKey(br => new { br.BookId, br.ReaderId });

            builder.HasOne(br => br.Book)
                .WithMany(b => b.BookReaders)
                .HasForeignKey(br => br.BookId);

            builder.HasOne(br => br.Reader)
                .WithMany(r => r.BookReaders)
                .HasForeignKey(br => br.ReaderId);

            builder.HasData(
                new { BookId = 101, ReaderId = 101, BorrowedDate = new DateTime(2024, 2, 1), ReturnedDate = (DateTime?)new DateTime(2024, 2, 15) },
                new { BookId = 102, ReaderId = 101, BorrowedDate = new DateTime(2024, 3, 22), ReturnedDate = (DateTime?)null },
                new { BookId = 103, ReaderId = 102, BorrowedDate = new DateTime(2024, 4, 5), ReturnedDate = (DateTime?)new DateTime(2024, 4, 20) },
                new { BookId = 104, ReaderId = 103, BorrowedDate = new DateTime(2024, 7, 1), ReturnedDate = (DateTime?)null },
                new { BookId = 101, ReaderId = 103, BorrowedDate = new DateTime(2024, 8, 10), ReturnedDate = (DateTime?)null }
            );
        }
    }
}
