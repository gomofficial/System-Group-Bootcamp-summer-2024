namespace ThirdBootcamp.LiveCoding.Catalog;

public class PaperBook : Book
{
    public float Weight { get; private init; }
    
    public PaperBook(string title, string description, string author) : base(title, description, author)
    {
    }

    public override decimal GetDeliveryCost()
    {
        return (decimal) Weight * 10;
    }
}