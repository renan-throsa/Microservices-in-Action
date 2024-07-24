
using Serilog;
using Serilog.Sinks.Elasticsearch;
using ShoppingCart.Service;
using ShoppingCart.Utils;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddTypedClient(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutomapperConfig));

if (builder.Environment.IsProduction())
{
    builder.Services.AddHostedService<QueueHostedService>();
}


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

app.MapControllers();

app.Run();
