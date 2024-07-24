namespace Task_2_Books.Catalog;

public abstract class Book
{
    protected Book(long id, string title, decimal price, string isbn)
    {
        ID = id;
        Title = title;
        Price = price;
        ISBN = isbn;
    }

    protected Book()
    {
        
    }
    public long ID { get; init; }
    public string Title { get; private init; }   

    private decimal _price;
    public decimal Price
    {
        get => _price;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");
            _price = value;
        }
    }
    
    public string ISBN { get; private set; }


    public void ChangePrice(decimal newPrice)
    {
        try
        {
            Price = newPrice; // Set the new price
            Console.WriteLine($"Price updated to: {Price:C}");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }
    }  //

    public override string ToString()
    {
        return $"{this.Title} {this.Price}";
    }  //
}
