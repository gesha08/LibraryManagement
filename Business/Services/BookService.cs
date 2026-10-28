using Library.Data;
using Library.Data.Entities;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync() =>
            await _context.Books
                .Include(b => b.Author)
                .ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) =>
            await _context.Books.FindAsync(id);

        public async Task<Book?> GetWithDetailsAsync(int id) =>
            await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookReaders)
                    .ThenInclude(br => br.Reader)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<List<Author>> GetAllAuthorsAsync() =>
            await _context.Authors.ToListAsync();

        public async Task<List<Reader>> GetAvailableReadersAsync() =>
            await _context.Readers
                .Where(r => !_context.BookReaders.Any(br =>
                    br.ReaderId == r.Id && br.ReturnedDate == null))
                .ToListAsync();

        public async Task CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BookExistsAsync(int id) =>
            await _context.Books.AnyAsync(b => b.Id == id);

        public async Task<bool> IsBorrowedAsync(int bookId) =>
            await _context.BookReaders.AnyAsync(br =>
                br.BookId == bookId && br.ReturnedDate == null);

        public async Task BorrowAsync(int bookId, int readerId)
        {
            var borrow = new BookReader
            {
                BookId = bookId,
                ReaderId = readerId,
                BorrowedDate = DateTime.Now
            };

            _context.BookReaders.Add(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnAsync(int bookId)
        {
            var currentBorrow = await _context.BookReaders
                .FirstOrDefaultAsync(br => br.BookId == bookId && br.ReturnedDate == null);

            if (currentBorrow != null)
            {
                currentBorrow.ReturnedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
