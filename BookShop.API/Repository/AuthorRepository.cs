using System.Data;
using BookShop.API.Contract;
using BookShop.API.DbContexts;
using BookShop.API.Entity;
using Dapper;

namespace BookShop.API.Repository
{
    public class AuthorRepository: IAuthor
    {
        private readonly DapperContext _context;
        private const string INSERT_AUTHOR = "[dbo].[InsertAuthor]";
        private const string UPDATE_AUTHOR = "[dbo].[UpdateAuthor]";
        private const string DELETE_AUTHOR = "[dbo].[DeleteAuthor]";
        private const string GET_AUTHOR_BY_ID = "[dbo].[GetAuthorById]";
        private const string GET_AUTHOR_ALL = "[dbo].[GetAuthorAll]";
        public AuthorRepository(DapperContext context) 
        {
            _context = context;
        }
        public async Task<int> InsertAuthor(Author author)
        {
            try
            {
                var dateBirth = author.Birthdate.ToShortDateString() == "01-01-0001" ? null : author.Birthdate.ToShortDateString();
                var procedureName = INSERT_AUTHOR;
                var parameters = new DynamicParameters();
                parameters.Add("Identifier", author.Identifier, DbType.String, ParameterDirection.Input);
                parameters.Add("FirstName", author.FirstName, DbType.String, ParameterDirection.Input);
                parameters.Add("LastName", author.LastName, DbType.String, ParameterDirection.Input);
                parameters.Add("Birthdate", dateBirth, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("CityOrigin", author.CityOrigin, DbType.String, ParameterDirection.Input);
                parameters.Add("Email", author.Email, DbType.String, ParameterDirection.Input);             
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
        public async Task<bool> UpdateAuthor(Author author)
        {
            try
            {
                var dateBirth = author.Birthdate.ToShortDateString() == "01-01-0001" ? null : author.Birthdate.ToShortDateString();
                var procedureName = UPDATE_AUTHOR;
                var parameters = new DynamicParameters();
                parameters.Add("Id", author.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Identifier", author.Identifier, DbType.String, ParameterDirection.Input);
                parameters.Add("FirstName", author.FirstName, DbType.String, ParameterDirection.Input);
                parameters.Add("LastName", author.LastName, DbType.String, ParameterDirection.Input);
                parameters.Add("Birthdate", dateBirth, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("CityOrigin", author.CityOrigin, DbType.String, ParameterDirection.Input);
                parameters.Add("Email", author.Email, DbType.String, ParameterDirection.Input);
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
        public async Task<bool> DeleteAuthor(Author author)
        {
            try
            {                
                var procedureName = DELETE_AUTHOR;
                var parameters = new DynamicParameters();
                parameters.Add("Id", author.Id, DbType.Int32, ParameterDirection.Input);              
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
        public async Task<Author> GetAuthorById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var author = await connection.QueryFirstOrDefaultAsync<Author>
                        (GET_AUTHOR_BY_ID, parameters, commandType: CommandType.StoredProcedure);
                    return author ?? new Author();
                }
            }
            catch (Exception)
            {
                return new Author();
            }
        }
        public async Task<IEnumerable<Author>> GetAuthorAll()
        {
            List<Author> authors = new List<Author>();
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    authors = (List<Author>)await connection.QueryAsync<Author>
                       (GET_AUTHOR_ALL, commandType: CommandType.StoredProcedure);
                    return authors;
                }
            }
            catch (Exception)
            {
                return authors;
            }
        }
    }
}
