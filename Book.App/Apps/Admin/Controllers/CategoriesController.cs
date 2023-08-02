using Book.Service.Dtos.Categories;
using Book.Service.Responses;
using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Book.App.Apps.Admin.Controllers
{
    [Route("api/admin/[controller]/[action]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryPostDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var result = await _categoryService.UpdateAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
