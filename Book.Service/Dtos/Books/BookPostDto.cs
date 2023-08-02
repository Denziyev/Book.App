using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Dtos.Books
{
    public record BookPostDto
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public IFormFile File { get; set; }
        public int CategoryId { get; set; }
    }
}
