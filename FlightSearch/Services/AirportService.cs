namespace FlightSearch.Services;

public class AirportService : IAirportService
{
    public IReadOnlyList<string>? GetDestinations(string origin)
    {
        if (!FlightSearch.Data.RouteData.IsValidOrigin(origin))
            return null;

        return FlightSearch.Data.RouteData.GetDestinations(origin);
    }

    public IReadOnlyList<string> GetAllOrigins()
    {
        return FlightSearch.Data.RouteData.GetAllOriginCodes();
    }
}
