using Library.Data.Entities;

namespace Business.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetWithDetailsAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<List<Reader>> GetAvailableReadersAsync();
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> BookExistsAsync(int id);
        Task<bool> IsBorrowedAsync(int bookId);
        Task BorrowAsync(int bookId, int readerId);
        Task ReturnAsync(int bookId);
    }
}
