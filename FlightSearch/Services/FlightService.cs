using FlightSearch.Data;
using FlightSearch.Models;

namespace FlightSearch.Services;

public class FlightService : IFlightService
{
    public IReadOnlyList<Flight> Search(string? origin, string? destination, DateTime? departureDate)
    {
        return FlightData.GetFlights(origin, destination, departureDate);
    }
}
