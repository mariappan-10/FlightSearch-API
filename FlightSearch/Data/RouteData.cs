namespace FlightSearch.Data;

/// <summary>
/// In-memory airport and route data for the demo.
/// </summary>
public static class RouteData
{
    /// <summary>
    /// All valid airport codes (origin and destination).
    /// </summary>
    private static readonly HashSet<string> AllAirportCodes = new(StringComparer.OrdinalIgnoreCase)
    {
        "BLR", "BOM", "DEL", "LON", "PAR", "LAX", "JFK", "DXB", "SIN", "SYD"
    };

    /// <summary>
    /// Origin -> list of destination codes (routes).
    /// </summary>
    private static readonly Dictionary<string, List<string>> Routes = new(StringComparer.OrdinalIgnoreCase)
    {
        ["BLR"] = new List<string> { "BOM", "DEL", "DXB", "LON", "SIN" },
        ["BOM"] = new List<string> { "BLR", "DEL", "DXB", "LON", "PAR" },
        ["DEL"] = new List<string> { "BLR", "BOM", "DXB", "LON", "SIN" },
        ["LON"] = new List<string> { "PAR", "LAX", "JFK", "DXB", "BLR", "BOM", "DEL" },
        ["PAR"] = new List<string> { "LON", "LAX", "DXB", "BOM" },
        ["LAX"] = new List<string> { "JFK", "LON", "SYD" },
        ["JFK"] = new List<string> { "LAX", "LON", "PAR" },
        ["DXB"] = new List<string> { "LON", "PAR", "SIN", "BLR", "BOM", "DEL" },
        ["SIN"] = new List<string> { "SYD", "DXB", "BLR", "DEL" },
        ["SYD"] = new List<string> { "LAX", "SIN" }
    };

    public static bool IsValidOrigin(string origin)
    {
        if (string.IsNullOrWhiteSpace(origin)) return false;
        return Routes.ContainsKey(origin.Trim().ToUpperInvariant());
    }

    public static bool IsValidAirportCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return false;
        return AllAirportCodes.Contains(code.Trim().ToUpperInvariant());
    }

    public static IReadOnlyList<string> GetDestinations(string origin)
    {
        var key = origin.Trim().ToUpperInvariant();
        if (!Routes.TryGetValue(key, out var destinations))
            return Array.Empty<string>();
        return destinations.OrderBy(d => d).ToList();
    }

    public static IReadOnlyList<string> GetAllOriginCodes()
    {
        return Routes.Keys.OrderBy(k => k).ToList();
    }
}
