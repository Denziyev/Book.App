using AutoMapper;
using Book.Core.Entities;
using Book.Core.Repositories;
using Book.Service.Dtos.Categories;
using Book.Service.Responses;
using Book.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Book.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ApiResponse> CreateAsync(CategoryPostDto dto)
        {
            if (await _repository.isExist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse { StatusCode = 404, Description = $"{dto.Name} already exsist" };
            }

            Category category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 200, Description = $"{dto.Name} is created successfully", items = category };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            Category? category = await _repository.GetByIdAsync(x => !x.IsDeleted && x.Id == id);
            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This category doesnt exist" };
            }
            category.IsDeleted = true;
            await _repository.Update(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 200, items = category };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IQueryable<Category> query = await _repository.GetAllAsync(x => !x.IsDeleted);
            List<CategoryGetDto> categories = new List<CategoryGetDto>();
            categories = await query.Select(x => new CategoryGetDto { Name = x.Name,Id=x.Id }).ToListAsync();

            return new ApiResponse { StatusCode = 200, items = categories };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Category? category = await _repository.GetByIdAsync(x => !x.IsDeleted && x.Id == id);
            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This category doesnt exist" };
            }
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
            return new ApiResponse { StatusCode = 200, items = dto };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CategoryUpdateDto dto)
        {

            if (await _repository.isExist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id == id))
            {
                return new ApiResponse { StatusCode = 404, Description = $"{dto.Name} already exsist" };
            }
            Category? updatecategory = await _repository.GetByIdAsync(x => !x.IsDeleted && x.Id == id);
            if (updatecategory == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This category doesnt exist" };
            }
            updatecategory.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 200, items = updatecategory };
        }

       
    }
}
