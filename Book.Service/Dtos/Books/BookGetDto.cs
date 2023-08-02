using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Dtos.Books
{
    public record BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public string Image { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
