namespace Book.Client.Dtos.Books
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class GetItems<T>
    {
        public List<T> Items { get; set; }
    }
}
