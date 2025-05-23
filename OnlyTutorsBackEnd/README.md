# OnlyTutors Backend

## Overview

This is the backend of the **OnlyTutors** platform. It is a RESTful API built using **ASP.NET Core 6** in **C#** and connects to a **PostgreSQL** database using **Dapper** and **EF Core**.

## Features

- Manage users, tutors, lessons, and registrations
- REST API for frontend integration
- Swagger UI for testing endpoints

## Tech Stack

- **C# / .NET 6**
- **ASP.NET Core Web API**
- **PostgreSQL**
- **Entity Framework Core** and **Dapper**
- **Swagger / OpenAPI**

## Setup Instructions

### Prerequisites

- .NET 6 SDK
- PostgreSQL database

### Configuration

Edit `appsettings.json` with your PostgreSQL connection string.

### Build and Run

```bash
dotnet build
dotnet run
```

API available at: `http://localhost:5000`

## Folder Structure

- `Controllers/`: API endpoints
- `Models/`: Entity and DTO classes
- `Repositories/`: Data access logic
- `appsettings.json`: Configuration

## License

MIT License
