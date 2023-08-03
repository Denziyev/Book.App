namespace Book.Client.Dtos.Categories
{
    public class CategoryGetDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class GetItems<T>
    {
        public List<T> Items { get; set; }
    }
}
