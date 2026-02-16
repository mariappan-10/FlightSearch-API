namespace FlightSearch.Data;

/// <summary>
/// In-memory sample flights for the demo.
/// </summary>
public static class FlightData
{
    private static readonly List<Models.Flight> Flights = new()
    {
        new Models.Flight { Id = "F1", Origin = "BLR", Destination = "LON", Departure = DateTime.UtcNow.AddDays(7), Carrier = "DemoAir", Price = 450 },
        new Models.Flight { Id = "F2", Origin = "BLR", Destination = "LON", Departure = DateTime.UtcNow.AddDays(8), Carrier = "DemoAir", Price = 420 },
        new Models.Flight { Id = "F3", Origin = "BLR", Destination = "DXB", Departure = DateTime.UtcNow.AddDays(5), Carrier = "DemoAir", Price = 280 },
        new Models.Flight { Id = "F4", Origin = "LON", Destination = "PAR", Departure = DateTime.UtcNow.AddDays(3), Carrier = "DemoAir", Price = 120 },
        new Models.Flight { Id = "F5", Origin = "LON", Destination = "LAX", Departure = DateTime.UtcNow.AddDays(10), Carrier = "DemoAir", Price = 520 },
        new Models.Flight { Id = "F6", Origin = "DEL", Destination = "DXB", Departure = DateTime.UtcNow.AddDays(4), Carrier = "DemoAir", Price = 320 },
        new Models.Flight { Id = "F7", Origin = "BOM", Destination = "LON", Departure = DateTime.UtcNow.AddDays(6), Carrier = "DemoAir", Price = 480 },
    };

    public static IReadOnlyList<Models.Flight> GetFlights(string? origin = null, string? destination = null, DateTime? departureDate = null)
    {
        var o = string.IsNullOrWhiteSpace(origin) ? null : origin.Trim().ToUpperInvariant();
        var d = string.IsNullOrWhiteSpace(destination) ? null : destination.Trim().ToUpperInvariant();
        var date = departureDate?.Date;

        return Flights
            .Where(f => o == null || string.Equals(f.Origin, o, StringComparison.OrdinalIgnoreCase))
            .Where(f => d == null || string.Equals(f.Destination, d, StringComparison.OrdinalIgnoreCase))
            .Where(f => !date.HasValue || f.Departure.Date == date.Value)
            .OrderBy(f => f.Departure)
            .ToList();
    }
}
