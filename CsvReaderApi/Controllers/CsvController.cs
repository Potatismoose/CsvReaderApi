using Microsoft.AspNetCore.Mvc;
using CsvReaderApi.Services;

namespace CsvReaderApi.Controllers;

[ApiController]
[Route("api/data")]
public class CsvController(ICsvService csvService) : ControllerBase
{
    private readonly ICsvService _csvService = csvService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetPersons([FromQuery] int? limit)
    {
        try
        {
            if (limit is int l && l <= 0)
                return BadRequest(new ProblemDetails
                {
                    Title = "Ogiltig parameter",
                    Status = 400,
                    Detail = "Limit måste vara ett positivt heltal."
                });                    

            var data = _csvService.ReadFileData(limit);

            if (data.Count == 0)
                return NoContent();

            return Ok(data);
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