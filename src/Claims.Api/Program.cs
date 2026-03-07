using Claims.Application.Claims;
using Claims.EventBus;
using Claims.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEventBus();

builder.Services.AddScoped<SubmitClaimHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/claims", async (
    SubmitClaimRequest request,
    SubmitClaimHandler handler,
    CancellationToken cancellationToken) =>
{
    var command = new SubmitClaimCommand(
        request.CustomerId,
        request.Type,
        request.Amount,
        request.Description);

    var claim = await handler.HandleAsync(command, cancellationToken);

    return Results.Created($"/claims/{claim.Id}", new ClaimResponse(
        claim.Id,
        claim.CustomerId,
        claim.Type,
        claim.Amount,
        claim.Description,
        claim.Status.ToString(),
        claim.CreatedAt,
        claim.UpdatedAt));
})
.WithName("SubmitClaim")
.WithOpenApi();

app.Run();

record SubmitClaimRequest(string CustomerId, string Type, decimal Amount, string? Description);

record ClaimResponse(
    Guid Id,
    string CustomerId,
    string Type,
    decimal Amount,
    string? Description,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);
