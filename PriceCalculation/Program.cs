using PriceCalculation.Utils;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies();
builder.Services.AddTypedClient(builder.Configuration);

builder.Host.UseSerilog((context, logger) =>
{
    logger.Enrich.FromLogContext();
    if (context.HostingEnvironment.IsDevelopment())
        logger.WriteTo.Console();
    else
        logger.WriteTo.Console(new JsonFormatter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
