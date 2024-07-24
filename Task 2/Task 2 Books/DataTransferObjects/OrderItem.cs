
namespace Task_2_Books.DataTransferObjects;
public class OrderItemDto
{
	public long ID { get; set; }
	public long OrderID { get; set; }
	public long BookID { get; set; }
	public int Quantity { get; set; }
}
