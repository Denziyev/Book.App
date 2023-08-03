using Book.Service.Responses;
using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.App.Apps.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse result = await _categoryService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            ApiResponse result = await _categoryService.GetAsync(Id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
