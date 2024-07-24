namespace Task_2_Books.Catalog;

public class AudioBook : Book
{
    public long TotalMinutes { get; set; }

    public AudioBook() : base()
    {
        
    }
    public AudioBook(long id, string title, decimal price, string isbn, long totalMinutes) : base(id, title, price, isbn)
    {
        TotalMinutes = totalMinutes;
    }

}