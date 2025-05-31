using CsvReaderApi.Services;

public class FileReader : IFileReader
{
    public IEnumerable<string> ReadLines(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("CSV-filen kunde inte hittas.");
        return File.ReadLines(path);
    }
}