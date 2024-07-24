using System.Collections;

namespace Task_IEnumerator;

public class TableEnumerator<T> : IEnumerator<T>
{
    private List<T> _items;
    private int _currentIndex = -1;

    public TableEnumerator(List<T> tableItems)
    {
        _items = tableItems;
    }

    public T Current => _items[_currentIndex];
    
    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        _currentIndex++;
        return _currentIndex < _items.Count;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }

    public void Dispose()
    {
        
    }
}