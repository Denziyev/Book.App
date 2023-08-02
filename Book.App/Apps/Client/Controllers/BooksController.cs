using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.App.Apps.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _BookService;

        public BooksController(IBookService BookService)
        {
            _BookService = BookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _BookService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _BookService.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
