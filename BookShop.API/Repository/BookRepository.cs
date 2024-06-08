using System.Data;
using BookShop.API.Contract;
using BookShop.API.DbContexts;
using BookShop.API.Entity;
using Dapper;

namespace BookShop.API.Repository
{
    public class BookRepository: IBook
    {
        private readonly DapperContext _context;
        private const string INSERT_BOOK = "[dbo].[InsertBook]";
        private const string UPDATE_BOOK = "[dbo].[UpdateBook]";
        private const string DELETE_BOOK = "[dbo].[DeleteBook]";
        private const string GET_BOOK_BY_ID = "[dbo].[GetBookById]";
        private const string GET_BOOK_ALL = "[dbo].[GetBookAll]";
        public BookRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> InsertBook(Book book)
        {
            try
            {              
                var procedureName = INSERT_BOOK;
                var parameters = new DynamicParameters();                
                parameters.Add("Title", book.Title, DbType.String, ParameterDirection.Input);
                parameters.Add("Year", book.Year, DbType.Int32, ParameterDirection.Input);
                parameters.Add("NumberPages", book.NumberPages, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IdAuthor", book.IdAuthor, DbType.Int32, ParameterDirection.Input);                
                using (var connection = _context.CreateConnection())
                {
                    var id = await connection.QueryFirstOrDefaultAsync<int>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return id;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<bool> UpdateBook(Book book)
        {
            try
            {
                var procedureName = UPDATE_BOOK;
                var parameters = new DynamicParameters();
                parameters.Add("Id", book.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Title", book.Title, DbType.String, ParameterDirection.Input);
                parameters.Add("Year", book.Year, DbType.Int32, ParameterDirection.Input);
                parameters.Add("NumberPages", book.NumberPages, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IdAuthor", book.IdAuthor, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var rowsAffected = await connection.QueryFirstOrDefaultAsync<bool>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteBook(Book book)
        {
            try
            {
                var procedureName = DELETE_BOOK;
                var parameters = new DynamicParameters();
                parameters.Add("Id", book.Id, DbType.Int32, ParameterDirection.Input);               
                using (var connection = _context.CreateConnection())
                {
                    var rowsAffected = await connection.QueryFirstOrDefaultAsync<bool>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<Book> GetBookById(int id)
        {            
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);                
                using (var connection = _context.CreateConnection())
                {
                    var book = await connection.QueryFirstOrDefaultAsync<Book>
                        (GET_BOOK_BY_ID, parameters, commandType: CommandType.StoredProcedure);
                    return book ?? new Book();
                }
            }
            catch (Exception)
            {               
                return new Book();
            }
        }
        public async Task<IEnumerable<Book>> GetBookAll()
        {
            List<Book> books = new List<Book>();
            try
            {                
                using (var connection = _context.CreateConnection())
                {
                    books = (List<Book>)await connection.QueryAsync<Book>
                       (GET_BOOK_ALL, commandType: CommandType.StoredProcedure);
                    return books;
                }
            }
            catch (Exception)
            {              
                return books;
            }
        }
    }
}
