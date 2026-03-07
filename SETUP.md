# Claims Processing System - Setup Guide

## 1. Docker Desktop (Required for PostgreSQL)

If you don't have Docker installed:

1. **Download** Docker Desktop for Windows: https://www.docker.com/products/docker-desktop/

2. **Install** and restart your computer if prompted.

3. **Verify** installation:
   ```powershell
   docker --version
   docker-compose --version
   ```

## 2. Start PostgreSQL Database

From the project root (`C:\Users\fcruz\Claims`):

```powershell
docker-compose up -d
```

This starts PostgreSQL on `localhost:5432` with:
- **Database:** ClaimsDb
- **Username:** postgres
- **Password:** postgres

To check if the container is running:
```powershell
docker ps
```

## 3. Apply Database Migrations

With PostgreSQL running:

```powershell
dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api
```

## 4. Run the API

```powershell
dotnet run --project src/Claims.Api
```

---

## Useful Commands

| Action | Command |
|--------|---------|
| Start PostgreSQL | `docker-compose up -d` |
| Stop PostgreSQL | `docker-compose down` |
| View logs | `docker-compose logs -f postgres` |
| Apply migrations | `dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api` |
| Add new migration | `dotnet ef migrations add MigrationName --project src/Claims.Infrastructure --startup-project src/Claims.Api --output-dir Persistence/Migrations` |
