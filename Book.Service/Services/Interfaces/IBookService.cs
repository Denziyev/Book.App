using Book.Service.Dtos.Books;
using Book.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Services.Interfaces
{
    public interface IBookService
    {
        public Task<ApiResponse> CreateAsync(BookPostDto product);
        public Task<ApiResponse> UpdateAsync(int id, BookUpdateDto product);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> DeleteAsync(int id);
    }
}
