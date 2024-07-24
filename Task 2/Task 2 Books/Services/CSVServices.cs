namespace Task_2_Books.Services;

public static class CsvReader
{
    /// <summary>
    ///     output => string array of column names of the CSV
    ///            => list of string array each item is a complete row of the CSV
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static (string[] headerRow, IEnumerable<string[]> csvLines) ReadHeaderAndCsvLines(string fileName)
    {
        var lines = File.ReadAllLines(Path.Combine("Datasets", fileName));
        var headerRow = lines.First().Split(',');
        var csvLines = lines.Skip(1).Select(x => x.Split(','));; 
        return (headerRow, csvLines);
    }

    /// <summary>
    /// output => returns a list of dict each represents a row with the column names as a key
    /// </summary>
    /// <param name="headerRow"></param>
    /// <param name="csvLines"></param>
    /// <returns></returns>
    public static List<Dictionary<string, string>> CreateListOfDictionaries(string[] headerRow, IEnumerable<string[]> csvLines)
    {
        var rows = new List<Dictionary<string, string>>();
        foreach (var line in csvLines)
        {
            var values = line;

            var rowDict = new Dictionary<string, string>();
            for (int i = 0; i < headerRow.Length; i++)
            {
                rowDict[headerRow[i]] = values[i];
            }
            rows.Add(rowDict);
        }
        return rows;
    }
}