using ProductCatalog.Utils;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthCheckesConfig();
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutomapperConfig));

builder.Host.UseSerilog((context, logger) =>
{
    logger.Enrich.FromLogContext().WriteTo.Console();
    if (context.HostingEnvironment.IsProduction())
        logger.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        }
        );
});

var app = builder.Build();
app.AddDataToDB();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecksConfig();

app.MapControllers();

app.Run();