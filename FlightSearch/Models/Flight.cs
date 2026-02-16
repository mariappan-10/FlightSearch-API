namespace FlightSearch.Models;

public class Flight
{
    public string Id { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime Departure { get; set; }
    public DateTime? ReturnAt { get; set; }
    public string Carrier { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
