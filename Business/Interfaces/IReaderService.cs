using Library.Data.Entities;

namespace Business.Interfaces
{
    public interface IReaderService
    {
        Task<List<Reader>> GetAllAsync();
        Task<Reader?> GetByIdAsync(int id);
        Task<Reader?> GetWithBooksAsync(int id);
        Task<int> GetBorrowedBookCountAsync(int readerId);
        Task CreateAsync(Reader reader);
        Task UpdateAsync(Reader reader);
        Task DeleteAsync(int id);
        Task<bool> ReaderExistsAsync(int id);
    }
}
