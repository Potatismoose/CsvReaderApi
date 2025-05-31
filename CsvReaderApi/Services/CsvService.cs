using CsvReaderApi.Models;

namespace CsvReaderApi.Services;

public class CsvService(IConfiguration config, IFileReader fileReader, IParsePerson personParser) : ICsvService
{
    private readonly IConfiguration _config = config;
    private readonly IFileReader _fileReader = fileReader;
    private readonly IParsePerson _personParser = personParser;

    public List<PersonDto> ReadFileData(int? limit)
    {
        var path = _config["Csv:FilePath"] ?? throw new Exception("CSV-filsökvägen saknas");

        var parsed = _fileReader
            .ReadLines(path)
            .Select(_personParser.Parse)
            .Where(p => p is not null)
            .Select(p => p!)
            .Select(p => new PersonDto
            {
                Name = p.Name,
                Age = p.Age,
                Email = p.Email
            });

        return limit is int l and > 0
            ? parsed.Take(l).ToList()
            : parsed.ToList();
    }
}