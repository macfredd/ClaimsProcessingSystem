# Development Session - Quick Start

Commands to run at the start of each development session.

---

## 1. Start PostgreSQL (Docker)

```powershell
cd C:\Users\fcruz\Claims
docker-compose up -d
```

**Verify container is running:**
```powershell
docker ps
```

---

## 2. Run the API

```powershell
cd C:\Users\fcruz\Claims
dotnet run --project src/Claims.Api
```

**API endpoints:**
- Swagger UI: http://localhost:5000/swagger (or the port shown in the console)

---

## 3. Useful Commands

### Connect to PostgreSQL (command line)

```powershell
docker exec -it claims-postgres psql -U postgres -d ClaimsDb
```

**Inside psql (tables use lowercase/snake_case):**
| Command | Description |
|---------|-------------|
| `\dt` | List all tables |
| `\d claims` | Describe claims table structure |
| `SELECT * FROM claims;` | Query claims |
| `\q` | Exit psql |

### Apply database migrations (when schema changes)

```powershell
cd C:\Users\fcruz\Claims
dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api
```

### Build the solution

```powershell
cd C:\Users\fcruz\Claims
dotnet build
```

---

## 4. End of Session (Optional)

### Stop PostgreSQL (keeps data in volume)

```powershell
docker-compose down
```

### Stop and remove data

```powershell
docker-compose down -v
```

---

## Quick Reference

| Action | Command |
|--------|---------|
| Start DB | `docker-compose up -d` |
| Stop DB | `docker-compose down` |
| Run API | `dotnet run --project src/Claims.Api` |
| Connect to DB | `docker exec -it claims-postgres psql -U postgres -d ClaimsDb` |
| Apply migrations | `dotnet ef database update --project src/Claims.Infrastructure --startup-project src/Claims.Api` |
