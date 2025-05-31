namespace CsvReaderApi.Services
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string path);
    }
}
