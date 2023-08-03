using AutoMapper;
using Book.Core.Entities;
using Book.Core.Repositories;
using Book.Service.Dtos.Books;
using Book.Service.Extentions;
using Book.Service.Responses;
using Book.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public BookService(IBookRepository repository, IMapper mapper, IWebHostEnvironment env, ICategoryRepository categoryRepository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
            _categoryRepository = categoryRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResponse> CreateAsync(BookPostDto dto)
        {
            Category category = await _categoryRepository.GetByIdAsync(x => x.Id == dto.CategoryId);
            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This category was not found" };
            }
            if (await _repository.isExist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse { StatusCode = 404, Description = $"{dto.Name} already exsist" };
            }
            Books Book = _mapper.Map<Books>(dto);
            Book.Image = dto.File.CreateImage(_env.WebRootPath, "assets/images/");
            Book.ImageURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + $"/assets/images/{Book.Image}";
            await _repository.AddAsync(Book);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 201, items = Book };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            Books Book = await _repository.GetByIdAsync(x => x.Id == id && !x.IsDeleted);
            if (Book == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This Book was not found" };
            }

            Book.IsDeleted = true;
            await _repository.Update(Book);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204, Description = "This Book deleted succesfully" };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IQueryable<Books> query = await _repository.GetAllAsync(x => !x.IsDeleted, "Category");
            List<BookGetDto> Books = new List<BookGetDto>();
            Books = await query.Select(x => new BookGetDto { Name = x.Name,Id=x.Id, ImageURL = x.ImageURL, Price = x.Price, Image = x.Image, CategoryId = x.CategoryId, CategoryName = x.Category.Name }).ToListAsync();

            return new ApiResponse { StatusCode = 200, items = Books };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Books Book = await _repository.GetByIdAsync(x => x.Id == id, "Category");
            if (Book == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This Book was not found" };
            }

            BookGetDto dto = _mapper.Map<BookGetDto>(Book);
            dto.CategoryName = Book.Category.Name;
            return new ApiResponse { StatusCode = 200, items = dto };
        }

        public async Task<ApiResponse> UpdateAsync(int id, BookUpdateDto dto)
        {
            Books Book = await _repository.GetByIdAsync(x => x.Id == id && !x.IsDeleted);
            if (Book == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "This Book was not found" };
            }

            Book.Name = dto.Name;
            Book.Price = dto.Price;
            Book.UpdatedAt = DateTime.Now;
            Book.CategoryId = dto.CategoryId;
            Book.Image = dto.File == null ? Book.Image : dto.File.CreateImage(_env.WebRootPath, $"assets/images/");
            Book.ImageURL = _contextAccessor.HttpContext.Request.Scheme + "://" + _contextAccessor.HttpContext.Request.Host + $"/assets/images/{Book.Image}";

            await _repository.Update(Book);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 204, Description = "Book changed succesfully", items = Book };

        }
    }
}
