namespace ThirdBootcamp.LiveCoding.Catalog;

public class Ebook : Book
{
    public long SizeInBytes { get; private init; }

    public Ebook(long sizeInBytes, string title, string description, string author) 
        : base(title, description, author)
    {
        SizeInBytes = sizeInBytes;
    }

    public override decimal GetDeliveryCost()
    {
        return 0;
    }
}