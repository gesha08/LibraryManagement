using Library.Data;
using Library.Data.Entities;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class ReaderService : IReaderService
    {
        private readonly LibraryDbContext _context;

        public ReaderService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reader>> GetAllAsync() =>
            await _context.Readers.ToListAsync();

        public async Task<Reader?> GetByIdAsync(int id) =>
            await _context.Readers.FindAsync(id);

        public async Task<Reader?> GetWithBooksAsync(int id) =>
            await _context.Readers
                .Include(r => r.BookReaders)
                    .ThenInclude(br => br.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<int> GetBorrowedBookCountAsync(int readerId) =>
            await _context.BookReaders.CountAsync(br =>
                br.ReaderId == readerId && br.ReturnedDate == null);

        public async Task CreateAsync(Reader reader)
        {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reader reader)
        {
            _context.Readers.Update(reader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reader = await _context.Readers.FindAsync(id);

            if (reader != null)
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ReaderExistsAsync(int id) =>
            await _context.Readers.AnyAsync(r => r.Id == id);
    }
}
