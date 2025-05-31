using CsvReaderApi.Models;

namespace CsvReaderApi.Services
{
    public interface ICsvService
    {
        List<PersonDto> ReadFileData(int? limit);
    }
}