using System.Text.Json.Serialization;
using Claims.Application.Events;
using Claims.Application;
using Claims.Domain.Events;
using Claims.EventBus;
using Claims.Infrastructure;
using Claims.RulesEngine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEventBus();
builder.Services.AddRulesEngine();
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Subscribe event handlers
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ClaimSubmitted>(async (evt, _) =>
{
    await Task.CompletedTask;
    Console.WriteLine($"[ClaimSubmitted] ClaimId: {evt.ClaimId}, CustomerId: {evt.CustomerId}, Amount: {evt.Amount}");
});
eventBus.Subscribe<ClaimApproved>(async (evt, _) =>
{
    await Task.CompletedTask;
    Console.WriteLine($"[ClaimApproved] ClaimId: {evt.ClaimId}, WorkOrderId: {evt.WorkOrderId}");
});
eventBus.Subscribe<ClaimRejected>(async (evt, _) =>
{
    await Task.CompletedTask;
    Console.WriteLine($"[ClaimRejected] ClaimId: {evt.ClaimId}, Reason: {evt.Reason}");
});
eventBus.Subscribe<WorkOrderCreated>(async (evt, _) =>
{
    await Task.CompletedTask;
    Console.WriteLine($"[WorkOrderCreated] WorkOrderId: {evt.WorkOrderId}, ClaimId: {evt.ClaimId}");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
