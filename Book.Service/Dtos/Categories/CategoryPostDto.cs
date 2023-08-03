using System.ComponentModel.DataAnnotations;

namespace Book.Service.Dtos.Categories
{
    public record CategoryPostDto
    {
        public string Name { get; set; }
    }
}
