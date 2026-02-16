# Flight Search Demo

A minimal flight search prototype with a **.NET Web API** backend and optional frontend.

## Backend (ASP.NET Core Web API)

### Run the API

From the solution directory:

```bash
cd FlightSearch
dotnet run
```

Or from the repo root:

```bash
dotnet run --project FlightSearch
```

The API listens on:
- **HTTP:** http://localhost:5215  
- **HTTPS:** https://localhost:7002  

(Exact ports may differ; check the console output.)

**Swagger UI:** In Development, open **https://localhost:7002/swagger** (or **http://localhost:5215/swagger**) in your browser to explore and test the API.

### API Endpoints

#### 1. Get destination airports for an origin

```http
GET /api/airports/destinations/{origin}
```

| Response | Condition |
|----------|-----------|
| **200 OK** | Returns JSON array of destination codes, e.g. `["LON","PAR","LAX","BLR"]` |
| **400 Bad Request** | Missing or invalid origin (e.g. not a 3-letter code) |
| **404 Not Found** | Unknown origin airport code |

**Example requests:**

```bash
# Success
curl http://localhost:5215/api/airports/destinations/BLR

# Success (sample response)
# ["BOM","DEL","DXB","LON","SIN"]

# Unknown origin → 404
curl -i http://localhost:5215/api/airports/destinations/XXX

# Invalid (empty) → 400
curl -i http://localhost:5215/api/airports/destinations/
```

#### 2. Get all origin airports (for dropdowns)

```http
GET /api/airports/origins
```

**200 OK** — JSON array of origin codes, e.g. `["BLR","BOM","DEL","DXB",...]`

```bash
curl http://localhost:5215/api/airports/origins
```

#### 3. Search flights

```http
GET /api/flights?origin={origin}&destination={destination}&departureDate={date}
```

All query parameters are optional. **200 OK** returns a JSON array of flight objects.

**Example:**

```bash
curl "http://localhost:5215/api/flights?origin=BLR&destination=LON"
```

### Data

- **Airports & routes:** In-memory data in `FlightSearch/Data/RouteData.cs`. Valid origins: BLR, BOM, DEL, LON, PAR, LAX, JFK, DXB, SIN, SYD.
- **Flights:** In-memory sample flights in `FlightSearch/Data/FlightData.cs`.

### Logging and errors

- Each request to the airports and flights endpoints is logged with timestamp and relevant parameters (e.g. origin).
- Errors (invalid input, unknown origin, unhandled exceptions) are logged and return appropriate HTTP status codes and JSON messages.

## Frontend

(To be added: simple search form with origin/destination dropdowns and search button, calling the backend API.)

## Repository

Source code can be published to a public Git repository (e.g. GitHub). Clone and run as above.
