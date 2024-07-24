/* Customer, Book (EBook, AudioBook, PaperBook)
 Publisher, Order, Author, Category, BookRanking, DeliveryMethod,
 Comment, PaymentMethod, Discount, Admin
*/

using ThirdBootcamp.LiveCoding.Ordering;
using ThirdBootcamp.LiveCoding.Catalog;

namespace ThirdBootcamp.LiveCoding;

public static class Program
{
  public static void Main(string[] args)
  {
      Book book1 = new Ebook(22,"EBook1", "this is book1", "system Group");
      Book book2 = new AudioBook("AudioBook2", "this is book1", "system Group");


      Order order = new Order();
      order.AddItem(book1);
      order.AddItem(book2);
      
      Console.WriteLine(order.ToString());
      
      order.RemoveItem(book1);
      
      Console.WriteLine(order.ToString());

  }
}