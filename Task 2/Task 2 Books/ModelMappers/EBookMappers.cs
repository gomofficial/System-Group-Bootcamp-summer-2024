using Task_2_Books.Catalog;
using Task_2_Books.DataTransferObjects;
using Task_2_Books.Interfaces;

namespace Task_2_Books.ModelMappers;

public class EBookMappers :IMapper<EBookDto,Ebook>
{
    public Ebook Map(EBookDto dto)
    {
        return new Ebook(dto.ID, dto.Title, dto.Price, dto.ISBN, dto.Size);
    }
}