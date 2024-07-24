namespace ThirdBootcamp_LiveCoding;

public static class Predicator
{
    public static List<int> Select(List<int> numbers, Predicate<int> predicate)
    {
        var results = new List<int>();
        foreach (var number in numbers)
        { 
            if(predicate.Invoke(number)) results.Add(number);
        }
        return results;
    }
}