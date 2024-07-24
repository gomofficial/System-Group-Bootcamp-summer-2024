using Task_2_Books.Catalog;

namespace Task_2_Books.Ordering;

public class OrderItem
{
    public OrderItem(Book book)
    {
        Book = book;
        Price = book.Price;
    }

    public override string ToString()
    {
        return $"Info: {this.Book.ToString()} Price: {this.Price} \n";
    }
    
    public Book Book { get; private init; }
    public decimal Price { get; private init; }
}