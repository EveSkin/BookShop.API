using BookShop.API.Entity;

namespace BookShop.API.Contract
{
    public interface IAuthor
    {
        public Task<int> InsertAuthor(Author author);
        public Task<bool> UpdateAuthor(Author author);
        public Task<bool> DeleteAuthor(Author author);
        public Task<Author> GetAuthorById(int id);
        public Task<IEnumerable<Author>> GetAuthorAll();
    }
}
