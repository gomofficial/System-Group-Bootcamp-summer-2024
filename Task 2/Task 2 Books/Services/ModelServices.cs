using Task_2_Books.Catalog;
using Task_2_Books.Interfaces;

namespace Task_2_Books.Services;

public class ModelServices<T, TDto> where T : new()
{
    /// <summary>
    /// Maps a Data Transfer Object to Model Instance
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>

    private readonly IMapper<TDto, T> _mapper;

    public ModelServices(IMapper<TDto, T> mapper)
    {
        _mapper = mapper;
    }

    private T MapDtoToModel (TDto dto)
    {
        T model;
        model = _mapper.Map(dto);
        return model;
    }
    
    /// <summary>
    /// Maps a Data Transfer Object Model instances
    /// </summary>
    /// <param name="dtoList  "></param>
    /// <returns></returns>
    public List<T> MapDtOtoDomainList(List<TDto> dtoList)
    {
        List<T> domains = new();
        foreach (var dto in dtoList)
        {
            var domain = MapDtoToModel(dto);
            domains.Add(domain);
        }
        return domains;
    }
}