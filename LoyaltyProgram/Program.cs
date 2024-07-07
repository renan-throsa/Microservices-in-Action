using LoyaltyProgram.Data;
using LoyaltyProgram.Service;
using LoyaltyProgram.Utils;
using Microsoft.Net.Http.Headers;
using Polly;
using Quartz;
using Quartz.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1 - Add services to the container.

var clientSettingsSection = builder.Configuration.GetSection(nameof(ClientSettings));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEventStore, EventStore>();
builder.Services.AddTransient<ILoyaltyProgramUserStore, LoyaltyProgramUserStore>();
builder.Services
    .AddHttpClient("events", (HttpClient client) =>
    {
        string address = clientSettingsSection.Get<ClientSettings>().Route; client.BaseAddress = new Uri(address); client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    })
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3)}));




builder.Services.Configure<ClientSettings>(clientSettingsSection);

builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});


builder.Services.AddQuartz(q =>
{

    var jobKey = new JobKey("EventsFetching");
    q.AddJob<SpecialOffersClient>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts =>
        opts
        .ForJob(jobKey)
        .WithIdentity(jobKey.Name + " trigger")
        .StartAt(DateTime.Now.AddSeconds(30))
        .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(5)).RepeatForever())
    ); ;

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
