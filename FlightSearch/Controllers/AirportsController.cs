using Microsoft.AspNetCore.Mvc;
using FlightSearch.Services;

namespace FlightSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController : ControllerBase
{
    private readonly IAirportService _airportService;
    private readonly ILogger<AirportsController> _logger;

    public AirportsController(IAirportService airportService, ILogger<AirportsController> logger)
    {
        _airportService = airportService;
        _logger = logger;
    }

    /// <summary>
    /// Get destination airport codes for a given origin.
    /// </summary>
    /// <param name="origin">Origin airport code (e.g. BLR, LON).</param>
    /// <response code="200">Returns the list of destination codes.</response>
    /// <response code="400">Invalid origin (e.g. empty or invalid format).</response>
    /// <response code="404">Origin airport is unknown.</response>
    [HttpGet("destinations/{origin}")]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDestinations(string origin)
    {
        var timestamp = DateTime.UtcNow;
        _logger.LogInformation("[{Timestamp:O}] Request: GET /api/airports/destinations/{Origin}", timestamp, origin);

        if (string.IsNullOrWhiteSpace(origin))
        {
            _logger.LogWarning("Bad Request: origin parameter is missing or empty");
            return BadRequest(new { message = "Origin airport code is required and cannot be empty." });
        }

        var normalizedOrigin = origin.Trim().ToUpperInvariant();
        if (normalizedOrigin.Length != 3)
        {
            _logger.LogWarning("Bad Request: invalid origin format '{Origin}' (expected 3-letter code)", origin);
            return BadRequest(new { message = "Origin must be a valid 3-letter airport code." });
        }

        var destinations = _airportService.GetDestinations(normalizedOrigin);
        if (destinations == null)
        {
            _logger.LogWarning("Not Found: unknown origin '{Origin}'", normalizedOrigin);
            return NotFound(new { message = $"Origin airport '{normalizedOrigin}' is not found." });
        }

        var result = destinations.ToArray();
        _logger.LogInformation("[{Timestamp:O}] Response: 200 OK for origin {Origin}, destinations count: {Count}",
            DateTime.UtcNow, normalizedOrigin, result.Length);
        return Ok(result);
    }

    /// <summary>
    /// Get all origin airport codes (for populating origin dropdown).
    /// </summary>
    [HttpGet("origins")]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
    public IActionResult GetAllOrigins()
    {
        _logger.LogInformation("[{Timestamp:O}] Request: GET /api/airports/origins", DateTime.UtcNow);
        var origins = _airportService.GetAllOrigins().ToArray();
        _logger.LogInformation("[{Timestamp:O}] Response: 200 OK, origins count: {Count}", DateTime.UtcNow, origins.Length);
        return Ok(origins);
    }
}
