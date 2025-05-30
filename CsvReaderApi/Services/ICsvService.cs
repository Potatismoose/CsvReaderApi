using CsvReaderApi.Models;

namespace CsvReaderApi.Services
{
    public interface ICsvService
    {
        List<Person> ReadFileData();
    }
}