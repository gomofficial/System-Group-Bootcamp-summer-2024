namespace Task_2_Books.Catalog;

public class Ebook : Book
{
    public decimal Size { get; private init; }

    public Ebook():base()
    {
        
    }
    
    public Ebook(long id, string title, decimal price, string isbn, decimal size) 
        : base(id, title, price, isbn)
    {
        Size = size;
    }
    

}