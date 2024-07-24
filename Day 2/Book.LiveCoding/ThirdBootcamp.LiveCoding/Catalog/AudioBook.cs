namespace ThirdBootcamp.LiveCoding.Catalog;

public class AudioBook : Book
{
    public long SizeInBytes { get; set; }
    public TimeSpan Duration { get; set; }
    public AudioBook(string title, string description, string author) : base(title, description, author)
    {
    }

    public override decimal GetDeliveryCost()
    {
        return 0;
    }
}