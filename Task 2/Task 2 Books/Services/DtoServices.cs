namespace Task_2_Books.Services;

public class DtoServices<T> where T : new()
{
    /// <summary>
    /// turns a dictionary of data to a Data Transfer Object
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private T DictToDto(Dictionary<string,string> item)
    {
        var dto = new T();
        foreach (var kvp in item)
        {
            var property = typeof(T).GetProperty(kvp.Key);
            if (property != null)
            {
                var convertedValue = Convert.ChangeType(kvp.Value, property.PropertyType);
                property.SetValue(dto,convertedValue);
            }
        }
        return dto;
    }
    
    /// <summary>
    /// Maps a list of data dictionaries to Data Transfer Object
    /// </summary>
    /// <param name="dicts"></param>
    /// <returns></returns>
    public List<T> DictToDtoList(List<Dictionary<string, string>> dicts)
    {
        List<T> dtoList = new();
        foreach (var item in dicts)
        {
            var dto = DictToDto(item);
            dtoList.Add(dto);
        }
        return dtoList;
    }
}