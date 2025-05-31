using CsvReaderApi.Models;

namespace CsvReaderApi.Services
{
    public class ParsePerson : IParsePerson
    {
        private const int ExpectedColumnCount = 4;

        public Person? Parse(string line)
        {
            var parts = line.Split(';');
            if (parts.Length != ExpectedColumnCount) return null;
            if (!int.TryParse(parts[0], out int id)) return null;
            if (!int.TryParse(parts[2], out int age)) return null;

            return new Person
            {
                Id = id,
                Name = parts[1],
                Age = age,
                Email = parts[3]
            };
        }
    }
}
