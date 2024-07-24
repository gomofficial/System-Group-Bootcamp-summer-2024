
namespace Task_2_Books.DataTransferObjects;
public class EBookDto
{
	public long ID { get; set; }
	public string Title { get; set; }
	public string ISBN { get; set; }
	public decimal Price { get; set; }
	public decimal Size { get; set; }
}
