using BookShop.API.Contract;
using BookShop.API.Entity;
using BookShop.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers
{
    /// <summary>
    /// Author: Evelyn Namoncura
    /// Date: 07-06-2024
    /// Description: Prueba técnica
    /// </summary>
    [Route("api/book")]
    public class BookAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IBook _bookInterface;
        public BookAPIController(IBook bookInterface)
        {
            _bookInterface = bookInterface;
            this._response = new ResponseDTO();
        }
        [HttpPost]
        [Route("InsertBook")]
        public async Task<object> InsertBook([FromBody] Book book)
        {
            try
            {
                var id = await _bookInterface.InsertBook(book);
                _response.Result = id;
                if (id > 0)
                {
                    _response.IsSuccess = true;
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]        
        [Route("UpdateBook")]
        public async Task<object> UpdateBook([FromBody] Book book)
        {
            try
            {
                _response.IsSuccess = await _bookInterface.UpdateBook(book);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<object> DeleteBook([FromBody] Book book)
        {
            try
            {
                _response.IsSuccess = await _bookInterface.DeleteBook(book);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [Route("GetBookById")]
        public async Task<object> GetBook([FromBody] int id)
        {
            try
            {
                var books = await _bookInterface.GetBookById(id);
                _response.Result = books;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = 0;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("GetBookAll")]
        public async Task<object> GetBooks()
        {
            try
            {
                var books = await _bookInterface.GetBookAll();
                _response.Result = books;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Result = 0;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
