using CsvReaderApi.Models;

namespace CsvReaderApi.Services;

public class CsvService : ICsvService
{
    private readonly string _filePath;
    private readonly CsvHeaderIndex _index = new();

    public CsvService(IConfiguration configuration)
    {
        _filePath = configuration["Csv:FilePath"] ?? throw new Exception("CSV filsökvägen är inte konfigurerad");
    }

    public List<Person> ReadFileData()
    {
        if (!File.Exists(_filePath))
            throw new FileNotFoundException("CSV-filen kunde inte hittas.");

        var lines = File.ReadAllLines(_filePath);
        if (lines.Length == 0)
            return Enumerable.Empty<Person>().ToList();

        return lines
            .Select(ParseLine)
            .Where(p => p is not null)
            .Select(p => p!)
            .ToList();
    }

    private Person? ParseLine(string line)
    {
        var parts = line.Split(';');

        if (parts.Length != 4) return null;
        if (!int.TryParse(parts[_index.Id], out int id)) return null;
        if (!int.TryParse(parts[_index.Age], out int age)) return null;

        return new Person
        {
            Id = id,
            Name = parts[_index.Name],
            Age = age,
            Email = parts[_index.Email]
        };
    }
}