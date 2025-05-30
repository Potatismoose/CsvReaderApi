using Microsoft.AspNetCore.Mvc;
using CsvReaderApi.Services;

namespace CsvReaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CsvController : ControllerBase
{
    private readonly ICsvService _csvService;

    public CsvController(ICsvService csvService)
    {
        _csvService = csvService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromQuery] int? limit)
    {
        try
        {
            if (limit.HasValue && limit.Value <= 0)
                return BadRequest(new { error = "Limit måste vara ett positivt heltal." });

            var data = _csvService.ReadFileData();

            if (!data.Any())
                return NoContent();

            var result = limit is int l ? data.Take(l) : data;
            return Ok(result);
        }
        catch (FileNotFoundException)
        {
            return StatusCode(500, new { error = "CSV-filen saknas." });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ett oväntat fel uppstod." });
        }
    }
}