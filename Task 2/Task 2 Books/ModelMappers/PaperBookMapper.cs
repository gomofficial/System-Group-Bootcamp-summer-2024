using Task_2_Books.Catalog;
using Task_2_Books.DataTransferObjects;
using Task_2_Books.Interfaces;

namespace Task_2_Books.ModelMappers;

public class PaperBookMapper : IMapper<PaperBookDto, PaperBook>
{
    public PaperBook Map(PaperBookDto dto)
    {
        return new PaperBook(dto.ID, dto.Title, dto.Price, dto.ISBN, dto.TotalPages);
    }
}