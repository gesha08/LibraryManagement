using Library.Data.Entities;

namespace Business.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author?> GetWithBooksAsync(int id);
        Task<int> GetBookCountByAuthorAsync(int authorId);
        Task CreateAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
        Task<bool> AuthorExistsAsync(int id);
    }
}
