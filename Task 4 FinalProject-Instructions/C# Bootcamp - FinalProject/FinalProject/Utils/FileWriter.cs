namespace FinalProject.Utils;

public class FileWriter
{
    public static void WriteList(List<string> items, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (string str in items)
            {
                writer.WriteLine(str);
            }
        }
    }

}