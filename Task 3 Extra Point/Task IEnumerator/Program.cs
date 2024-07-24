namespace Task_IEnumerator;

public class Program
{
    public static void Main(string[] args)
    {
        var table = new Table<int>();
        table.Add(2);
        table.Add(3);
        
        foreach (var item in table)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(table.Any(number => number > 10));
    }
}