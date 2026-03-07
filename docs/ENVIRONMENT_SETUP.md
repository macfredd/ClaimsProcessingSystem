# New Development Environment Setup

Instructions to set up the Claims Processing System on a new machine from scratch.

---

## Prerequisites

| Requirement | Version | Download |
|-------------|---------|----------|
| .NET SDK | 8.0+ | https://dotnet.microsoft.com/download/dotnet/8.0 |
| Docker Desktop | Latest | https://www.docker.com/products/docker-desktop/ |
| Git | Latest | https://git-scm.com/download/win |
| IDE | VS Code / Visual Studio | Optional |

---

## Step 1: Install .NET 8 SDK

1. Download: https://dotnet.microsoft.com/download/dotnet/8.0
2. Run the installer.
3. Verify:
   ```powershell
   dotnet --version
   ```

---

## Step 2: Install Docker Desktop

1. Download: https://www.docker.com/products/docker-desktop/
2. Run the installer.
3. **If prompted for WSL:**
   - Open PowerShell as Administrator.
   - Run:
   ```powershell
   wsl.exe --update
   ```
   - Restart the computer if required.
4. Start Docker Desktop and wait until it is fully running.
5. Verify:
   ```powershell
   docker --version
   docker run hello-world
   ```

---

## Step 3: Install EF Core Tools (Global)

```powershell
dotnet tool install --global dotnet-ef
```

Verify:
```powershell
dotnet ef --version
```

---

## Step 4: Clone the Project (if using Git)

**SSH (recommended - use when you have multiple GitHub accounts):**
```powershell
git clone git@github.com:macfredd/ClaimsProcessingSystem.git C:\Users\<YourUser>\Claims
cd C:\Users\<YourUser>\Claims
```

**HTTPS:**
```powershell
git clone https://github.com/macfredd/ClaimsProcessingSystem.git C:\Users\<YourUser>\Claims
cd C:\Users\<YourUser>\Claims
```

Or copy the project folder to your workspace.

---

## Step 5: Restore and Build

```powershell
cd C:\Users\fcruz\Claims
dotnet restore
dotnet build
```

---

## Step 6: Start PostgreSQL

```powershell
docker-compose up -d
```

Verify:
```powershell
docker ps
```

---

## Step 7: Apply Database Migrations

```powershell
dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api
```

---

## Step 8: Run the API

```powershell
dotnet run --project src/Claims.Api
```

Open Swagger: http://localhost:5000/swagger (check the port in the console output).

---

## Database Connection Details

| Setting | Value |
|---------|-------|
| Host | localhost |
| Port | 5432 |
| Database | ClaimsDb |
| Username | postgres |
| Password | postgres |
| Connection String | `Host=localhost;Port=5432;Database=ClaimsDb;Username=postgres;Password=postgres` |

---

## Useful Commands Reference

### Docker
```powershell
docker-compose up -d          # Start PostgreSQL
docker-compose down           # Stop PostgreSQL
docker ps                     # List running containers
docker-compose logs -f postgres   # View PostgreSQL logs
```

### Database
```powershell
# Connect to PostgreSQL
docker exec -it claims-postgres psql -U postgres -d ClaimsDb
```

### EF Core Migrations
```powershell
# Add new migration
dotnet ef migrations add MigrationName --project src/Claims.Infrastructure --startup-project src/Claims.Api

# Apply migrations
dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api

# Revert last migration
dotnet ef migrations remove --project src/Claims.Infrastructure --startup-project src/Claims.Api
```

### Project
```powershell
dotnet build                              # Build solution
dotnet run --project src/Claims.Api       # Run API
dotnet test                               # Run tests (when available)
```

---

## Troubleshooting

### Docker: "pipe docker_engine not found"
- Ensure Docker Desktop is running and fully started.
- Try running PowerShell as Administrator.

### Cannot connect to PostgreSQL
- Verify container is running: `docker ps`
- Check port 5432 is not used by another service.

### Migration fails
- Ensure PostgreSQL is running: `docker-compose up -d`
- Verify connection string in `src/Claims.Api/appsettings.json`.

### Git: "Permission denied" when pushing
- Use SSH instead of HTTPS if you have multiple GitHub accounts.
- See [GIT.md](GIT.md) for SSH setup and multi-account configuration.
