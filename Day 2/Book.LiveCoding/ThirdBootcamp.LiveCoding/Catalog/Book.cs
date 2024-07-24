using System.ComponentModel.DataAnnotations;

namespace ThirdBootcamp.LiveCoding.Catalog;

public abstract class Book
{
    protected Book(string title, string description, string author)
    {
        Title = title;
        Description = description;
        Author = author;
    }

    public long Id { get; init; }
    public string Title { get; private init; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Author { get; private init; }

    // public static Book Create(string title, string description, string author)
    // {
    //     
    // }

    public void ChangePrice(decimal price)
    {
        //TODO: check preconditions
        Price = price;
    }

    public override string ToString()
    {
        return $"{this.Title} {this.Price}";
    }

    public abstract decimal GetDeliveryCost();
}