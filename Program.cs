using KafkaProducerNetCore.Src.Application;
using KafkaProducerNetCore.Src.Models;
using KafkaProducerNetCore.Src.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Kakfa Configuration
 builder.Services.Configure<KafkaConfigurationOptions>(
    builder.Configuration.GetSection(KafkaConfigurationOptions.KafkaConfiguration));

builder.Services.AddSingleton(new KafkaService(builder.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () =>
{
    return "welcome";
});

app.MapPost("/message", (KafkaService kafkaService, MessageRequest request) => {

    var msg = new Message(request.Payload);

    var hasSent = kafkaService.SendEventAsync(msg);

    if (hasSent.Result)
        return Results.Created($"message/{msg.Id}", msg);

    return Results.Problem("Message not sent", msg.GetEvent().Item2, 502);
});

app.Run();