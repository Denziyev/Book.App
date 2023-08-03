namespace Book.Client.Dtos.Books
{
    public class BookUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile FormFile { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
