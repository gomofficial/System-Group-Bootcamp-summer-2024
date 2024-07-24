using System.Text;
using ThirdBootcamp.LiveCoding.Catalog;

namespace ThirdBootcamp.LiveCoding.Ordering;

public class Order
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public decimal TotalCost { get; private set; } = Decimal.Zero;
    public DateTime Date { get;  private init; } = DateTime.Now;
    
    public void AddItem(Book book)
    {
        if (_items.Count > 4)
        {
            throw new InvalidOperationException("");
        }

        var item = new OrderItem(book);
        _items.Add(item);

        TotalCost += item.Price + item.Book.GetDeliveryCost();
    }
    
    public void RemoveItem(Book book)
    {
        if (_items.Count <= 0)
        {
            throw new InvalidOperationException("");
        }
        foreach (OrderItem item in _items.ToList())
        {
            try
            {
                if (item.Book== book)
                {
                    _items.Remove(item);
                    TotalCost -= item.Price + item.Book.GetDeliveryCost();
                }
            }
            catch (Exception)
            {
                
            }
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (OrderItem item in _items)
        {
            sb.Append(item.ToString());
        }

        sb.Append($"Total Price: {this.TotalCost}");
        return sb.ToString();
    }
}

public enum PaymentMethod
{
    Online,
    Receipt,
}