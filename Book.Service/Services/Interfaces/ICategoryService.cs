using Book.Service.Dtos.Categories;
using Book.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Book.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<ApiResponse> CreateAsync(CategoryPostDto category);
        public Task<ApiResponse> UpdateAsync(int id, CategoryUpdateDto category);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> DeleteAsync(int id);
    }
}
