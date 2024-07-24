namespace Task_2_Books.Catalog;

public class PaperBook : Book
{
    public decimal TotalPages { get; private init; }

    public PaperBook() : base()
    {
        
    }
    
    public PaperBook(long id, string title, decimal price, string isbn, decimal totalPages) : base(id, title, price, isbn)
    {
        TotalPages = totalPages;
    }
}