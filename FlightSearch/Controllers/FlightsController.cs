using Microsoft.AspNetCore.Mvc;
using FlightSearch.Services;

namespace FlightSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly ILogger<FlightsController> _logger;

    public FlightsController(IFlightService flightService, ILogger<FlightsController> logger)
    {
        _flightService = flightService;
        _logger = logger;
    }

    /// <summary>
    /// Search flights by origin, destination, and optional departure date.
    /// </summary>
    /// <param name="origin">Origin airport code (optional).</param>
    /// <param name="destination">Destination airport code (optional).</param>
    /// <param name="departureDate">Departure date (optional, ISO date).</param>
    [HttpGet]
    [ProducesResponseType(typeof(Models.Flight[]), StatusCodes.Status200OK)]
    public IActionResult Search([FromQuery] string? origin, [FromQuery] string? destination, [FromQuery] DateTime? departureDate)
    {
        var timestamp = DateTime.UtcNow;
        _logger.LogInformation("[{Timestamp:O}] Request: GET /api/flights Origin={Origin}, Destination={Destination}, DepartureDate={DepartureDate}",
            timestamp, origin, destination, departureDate);

        var flights = _flightService.Search(origin, destination, departureDate);
        var result = flights.ToArray();

        _logger.LogInformation("[{Timestamp:O}] Response: 200 OK, flights count: {Count}",
            DateTime.UtcNow, result.Length);
        return Ok(result);
    }
}
