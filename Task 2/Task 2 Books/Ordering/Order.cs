using System.Text;
using Task_2_Books.Catalog;
using Task_2_Books.DataTransferObjects;

namespace Task_2_Books.Ordering;

public class Order
{
    public Order(OrderDto dto)
    {
        this.Date = dto.DateTime;
        this.ID = dto.ID;
    }
    
    public long ID { get; init; } 
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public decimal TotalCost { get; private set; }
    public DateTime Date { get;  private init; }
    
    public void AddItem(Book book)
    {
        var item = new OrderItem(book);
        _items.Add(item);

        TotalCost += item.Price;
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
                    TotalCost -= item.Price;
                }
            }
            catch (Exception)
            {
                throw new InvalidDataException("");
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