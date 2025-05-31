using CsvReaderApi.Models;

namespace CsvReaderApi.Services
{
    public interface IParsePerson
    {
        Person? Parse(string line);
    }
}
