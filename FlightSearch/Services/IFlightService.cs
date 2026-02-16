using FlightSearch.Models;

namespace FlightSearch.Services;

public interface IFlightService
{
    IReadOnlyList<Flight> Search(string? origin, string? destination, DateTime? departureDate);
}
