using LoyaltyProgram.Jobs;
using LoyaltyProgram.Utils;
using Quartz;
using Quartz.AspNetCore;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 1 - Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutomapperConfig));
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddTypedClient(builder.Configuration);


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

builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});


builder.Services.AddQuartz(q =>
{

    var jobKey = new JobKey("OffersFetching");
    q.AddJob<SpecialOffersJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts =>
        opts
        .ForJob(jobKey)
        .WithIdentity(jobKey.Name + " trigger")
        .StartAt(DateTime.Now.AddSeconds(10))
        .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromDays(1)).RepeatForever())
    );

});

var app = builder.Build();

// 2- Configure services

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
