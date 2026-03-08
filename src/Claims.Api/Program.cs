using Claims.EventBus;
using Claims.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEventBus();
builder.Services.AddScoped<Claims.Application.Claims.SubmitClaimHandler>();
builder.Services.AddScoped<Claims.Application.Claims.GetClaimByIdHandler>();

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
