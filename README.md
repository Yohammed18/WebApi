@"
# GameStore API

A RESTful API built with ASP.NET Core (.NET 10) for managing a video game catalog.

## Tech Stack
- C# / .NET 10
- ASP.NET Core Minimal APIs
- SQL Server

## Endpoints
| Method | Route | Description |
|---|---|---|
| GET | /games | Get all games |
| GET | /games/{id} | Get a game by id |
| POST | /games | Create a game |
| PUT | /games/{id} | Update a game |
| DELETE | /games/{id} | Delete a game |

## Run Locally
1. Clone the repo
2. Set your connection string with user secrets:
   dotnet user-secrets set "ConnectionStrings:GameStore" "<your-connection-string>" --project WebApi
3. Run the API:
   dotnet run --project WebApi
"@ | Out-File README.md -Encoding utf8