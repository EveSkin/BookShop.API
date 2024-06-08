namespace BookShop.API.Entity
{
    public class Book
    {
       public int Id { get; set; }
       public string Title { get; set; } = string.Empty;   
       public int Year { get; set; }
       public int NumberPages { get; set; }
       public int IdAuthor { get; set; }
    }
}
