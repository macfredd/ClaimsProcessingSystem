# Claims Processing System

Modular system for automatic claims processing (insurance, refunds, warranties, payment disputes).

## Tech Stack

- **.NET 8** | **C#** | **ASP.NET Core**
- **PostgreSQL** (Docker)
- **Entity Framework Core**
- **Clean Architecture** | **DDD-lite** | **Event-driven**

## Project Structure

```
src/
├── Claims.Api           # REST API
├── Claims.Application  # Use cases, services
├── Claims.Domain       # Entities, events, value objects
├── Claims.Infrastructure # Persistence, EF Core
├── Claims.RulesEngine  # Business rules evaluation
├── Claims.Workflow     # Process orchestration
├── Claims.WorkOrders   # Work order management
├── Claims.EventBus     # Event bus abstraction
└── Claims.Shared       # Shared utilities
```

## Quick Start

1. **Start PostgreSQL**
   ```powershell
   docker-compose up -d
   ```

2. **Apply migrations**
   ```powershell
   dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api
   ```

3. **Run the API**
   ```powershell
   dotnet run --project src/Claims.Api
   ```

See [docs/DEVELOPMENT_SESSION.md](docs/DEVELOPMENT_SESSION.md) for detailed setup.

## Documentation

- [Development Session](docs/DEVELOPMENT_SESSION.md) - Daily workflow
- [Environment Setup](docs/ENVIRONMENT_SETUP.md) - New machine setup
