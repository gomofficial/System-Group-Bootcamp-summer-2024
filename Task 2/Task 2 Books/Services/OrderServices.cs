using Task_2_Books.Catalog;
using Task_2_Books.DataTransferObjects;
using Task_2_Books.Ordering;
using Task_2_Books.Services;

namespace Task_2_Books.Services;

public class OrderServices
{
    private static Order DtoToOrder(OrderDto dto)
    {
        var order = new Order(dto);
        return order;
    }
    
    public static List<Order> DtoToOrderList(List<OrderDto> orderDtoList, List<OrderItemDto> orderItemDtoList, List<Book> bookList)
    {
        var orderList = new List<Order>();
        foreach (var dto in orderDtoList)
        {
            var order = DtoToOrder(dto);
            var orderItemDtos = orderItemDtoList.Where(item => item.OrderID == order.ID);
            
            foreach (var item in orderItemDtos)
            {
                Book matchingBook = bookList.Find(book => book.ID == item.BookID) ?? throw new InvalidOperationException();
                for (int i = 0; i < item.Quantity; i++)
                {
                    Console.WriteLine(matchingBook);
                    order.AddItem(matchingBook);
                }
                
            }
            orderList.Add(order);
        }

        return orderList;
    }
}