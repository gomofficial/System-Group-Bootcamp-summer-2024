
namespace Task_2_Books.DataTransferObjects;
public class AudioBookDto
{
	public long ID { get; set; }
	public string Title { get; set; }
	public string ISBN { get; set; }
	public decimal Price { get; set; }
	public int TotalMinutes { get; set; }
}