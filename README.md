# API Documentation

# HotelBookingManagement API

---
## Overview
This repository contains a .NET 8 Web API for a hotel booking service. It uses Entity Framework Core for data access.

## Tech Stack
- .NET 8
- Entity Framework Core
- Database provider detected: **SQLite (Microsoft.EntityFrameworkCore.Sqlite)**

---
## Getting Started (Run locally)
1. Ensure you have the .NET 8 SDK installed: https://dotnet.microsoft.com/download
2. From the project root (where the `.sln` or API `.csproj` is), run:

---
## Configuration
- Connection strings are located in `appsettings.json`.
- To use SQLite, the connection string typically looks like: `Data Source=hotelbooking.db`.

---
## API Documentation
- Swagger is enabled. Visit `/swagger` to explore endpoints.

### Available endpoints (inferred from controller source files)
#### AdminController.cs
- Controller: `AdminController`
- Route template: `api/admin`
- Actions:
  - `HTTPPOST` `api/admin/reset` — method `Reset` — params: ``
  - `HTTPPOST` `api/admin/seed` — method `Seed` — params: ``

#### BookingController.cs
- Controller: `BookingController`
- Route template: `api/booking`
- Actions:
  - `HTTPPOST` `api/booking` — method `Create` — params: `[FromBody] BookingRequestDto request`
  - `HTTPGET` `api/booking/{reference}` — method `Get` — params: `string reference`

#### HotelController.cs
- Controller: `HotelController`
- Route template: `api/hotel`
- Actions:
  - `HTTPGET` `api/hotel` — method `Get` — params: ``
  - `HTTPGET` `api/hotel/search` — method `Search` — params: `[FromQuery,Required] string name`
  - `HTTPGET` `api/hotel/{hotelId}/availability` — method `Availability` — params: `[FromRoute] int hotelId, [FromQuery] AvailabilityRequestDto requestDto`

---
## Common Troubleshooting
- If you see `This localhost page can’t be found`, check the port printed by `dotnet run` or `launchSettings.json`.
- Ensure database migrations are applied and connection strings are correct.
- If you accidentally committed `.vs/`, `bin/`, or `obj/`, add them to `.gitignore` and remove from tracking:

```bash
git rm -r --cached .vs bin obj
git commit -m "Remove IDE and build artifacts from repo"
```



