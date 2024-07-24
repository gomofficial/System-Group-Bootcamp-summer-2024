using Task_2_Books.Catalog;
using Task_2_Books.DataTransferObjects;
using Task_2_Books.Interfaces;
using Task_2_Books.ModelMappers;
using Task_2_Books.Ordering;
using Task_2_Books.Services;

namespace Task_2_Books;

public static class Program
{
  public static void Main(string[] args)
  {
    // Reading Ebooks CSV file and creating EBook Models
    var (header,rows) = CsvReader.ReadHeaderAndCsvLines("Ebooks.csv");
    var dictList = CsvReader.CreateListOfDictionaries(header, rows);
    DtoServices<EBookDto> ebookDtoHandler = new();
    var eBookDtos = ebookDtoHandler.DictToDtoList(dictList);
    var ebookHandler = new ModelServices<Ebook, EBookDto>(new EBookMappers());
    var eBooks = ebookHandler.MapDtOtoDomainList(eBookDtos);
    
    // Reading Audiobooks CSV file and creating EBook Models
    (header,rows) = CsvReader.ReadHeaderAndCsvLines("AudioBooks.csv");
    dictList = CsvReader.CreateListOfDictionaries(header, rows);
    DtoServices<AudioBookDto> audiobookDtoHandler = new();
    var audioBookDtos = audiobookDtoHandler.DictToDtoList(dictList);
    var audioBookHandler = new ModelServices<AudioBook, AudioBookDto>(new AudioBookMapper());
    var audioBooks = audioBookHandler.MapDtOtoDomainList(audioBookDtos);

    // Reading PaperBooks CSV file and creating EBook Models
    (header,rows) = CsvReader.ReadHeaderAndCsvLines("PaperBooks.csv");
    dictList = CsvReader.CreateListOfDictionaries(header, rows);
    DtoServices<PaperBookDto> paperbookDtoHandler = new();
    var paperBookDtos = paperbookDtoHandler.DictToDtoList(dictList);
    var paperBookHandler = new ModelServices<PaperBook, PaperBookDto>(new PaperBookMapper());
    var paperBooks = paperBookHandler.MapDtOtoDomainList(paperBookDtos);
    
    // Reading OrderItem CSV and creating OrderItem Data Transfer Objects
    (header,rows) = CsvReader.ReadHeaderAndCsvLines("OrderItems.csv");
    dictList = CsvReader.CreateListOfDictionaries(header, rows);
    DtoServices<OrderItemDto> orderItemDtoHandler = new();
    var orderItemDtos = orderItemDtoHandler.DictToDtoList(dictList);
    
    // Reading Order CSV and creating Order Data Transfer Objects
    (header,rows) = CsvReader.ReadHeaderAndCsvLines("Orders.csv");
    dictList = CsvReader.CreateListOfDictionaries(header, rows);
    DtoServices<OrderDto> orderDtoHandler = new();
    var orderDtos = orderDtoHandler.DictToDtoList(dictList);
    
    // List of Book Objects
    List<Book> books = new();
    books.AddRange(eBooks);
    books.AddRange(audioBooks);
    books.AddRange(paperBooks);
    
    // creating Order models
    var orders = OrderServices.DtoToOrderList(orderDtos, orderItemDtos, books);
    
    //Printing Orders
    foreach (var item in orders)
    {
      Console.WriteLine(item.ToString());
      Console.WriteLine("");
      Console.WriteLine("");
      Console.WriteLine("");
    }
  }
}