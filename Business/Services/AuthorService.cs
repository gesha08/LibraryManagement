using Library.Data;
using Library.Data.Entities;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _context;

        public AuthorService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync() =>
            await _context.Authors.ToListAsync();

        public async Task<Author?> GetByIdAsync(int id) =>
            await _context.Authors.FindAsync(id);

        public async Task<Author?> GetWithBooksAsync(int id) =>
            await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<int> GetBookCountByAuthorAsync(int authorId) =>
            await _context.Books.CountAsync(b => b.AuthorId == authorId);

        public async Task CreateAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AuthorExistsAsync(int id) =>
            await _context.Authors.AnyAsync(a => a.Id == id);
    }
}
