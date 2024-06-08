using Azure;
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
    [Route("api/author")]
    public class AuthorAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IAuthor _authorInterface;
        public AuthorAPIController(IAuthor authorInterface)
        {
            _authorInterface = authorInterface;
            this._response = new ResponseDTO();
        }
        [HttpPost]
        [Route("InsertAuthor")]
        public async Task<object> InsertAuthor([FromBody] Author author)
        {
            try
            {
                var id = await _authorInterface.InsertAuthor(author);
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
        [Route("UpdateAuthor")]
        public async Task<object> UpdateAuthor([FromBody] Author author)
        {
            try
            {
                _response.IsSuccess = await _authorInterface.UpdateAuthor(author);
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
        [Route("DeleteAuthor")]
        public async Task<object> DeleteAuthor([FromBody] Author author)
        {
            try
            {
                _response.IsSuccess = await _authorInterface.DeleteAuthor(author);
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
        [Route("GetAuthorById")]
        public async Task<object> GetAuthorById([FromBody] int id)
        {
            try
            {
                var authors = await _authorInterface.GetAuthorById(id);
                _response.Result = authors;
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
        [Route("GetAuthorAll")]
        public async Task<object> GetAuthorAll()
        {
            try
            {
                var authors = await _authorInterface.GetAuthorAll();
                _response.Result = authors;
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
