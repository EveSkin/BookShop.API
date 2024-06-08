using BookShop.API.Entity;

namespace BookShop.API.Contract
{
    public interface IBook
    {
        public Task<int> InsertBook(Book book);
        public Task<bool> UpdateBook(Book book);
        public Task<bool> DeleteBook(Book book);
        public Task<Book> GetBookById(int id);
        public Task<IEnumerable<Book>> GetBookAll();
    }
}
