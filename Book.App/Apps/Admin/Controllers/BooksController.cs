using Book.Service.Dtos.Books;
using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.App.Apps.Admin.Controllers
{
    [Route("api/admin/[controller]/[action]")]
    [Authorize(Roles ="Admin,SuperAdmin")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _BookService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookPostDto dto)
        {
            var result = await _BookService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BookUpdateDto dto)
        {
            var result = await _BookService.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
