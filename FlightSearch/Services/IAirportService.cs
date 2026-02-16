namespace FlightSearch.Services;

public interface IAirportService
{
    /// <summary>
    /// Gets destination airport codes for the given origin. Returns null if origin is unknown.
    /// </summary>
    IReadOnlyList<string>? GetDestinations(string origin);

    IReadOnlyList<string> GetAllOrigins();
}
