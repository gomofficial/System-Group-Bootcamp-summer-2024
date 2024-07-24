using Task_2_Books.Catalog;
using Task_2_Books.DataTransferObjects;
using Task_2_Books.Interfaces;

namespace Task_2_Books.ModelMappers;

public class AudioBookMapper:IMapper<AudioBookDto,AudioBook>
{
    public AudioBook Map(AudioBookDto dto)
    {
        return new AudioBook(dto.ID, dto.Title, dto.Price, dto.ISBN, dto.TotalMinutes);
    }
}