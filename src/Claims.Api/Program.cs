using Claims.EventBus;
using Claims.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEventBus();

// Register handlers for claims
builder.Services.AddScoped<Claims.Application.Claims.SubmitClaimHandler>();
builder.Services.AddScoped<Claims.Application.Claims.GetClaimByIdHandler>();

// Register handlers for work orders
builder.Services.AddScoped<Claims.Application.WorksOrders.CreateWorkOrderHandler>();
builder.Services.AddScoped<Claims.Application.WorksOrders.GetWorkOrderByIdHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
