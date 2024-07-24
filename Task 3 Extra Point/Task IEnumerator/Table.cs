using System.Collections;

namespace Task_IEnumerator;

public class Table<T> : IEnumerable<T>
{
    private List<T> _tableItems = new List<T>();

    public void Add(T item)
    {
        _tableItems.Add(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new TableEnumerator<T>(_tableItems);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
