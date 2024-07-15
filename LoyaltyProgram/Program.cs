using LoyaltyProgram.Filters;
using LoyaltyProgram.Jobs;
using LoyaltyProgram.Utils;
using Quartz;
using Quartz.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1 - Add services to the container.

var clientSettingsSection = builder.Configuration.GetSection(nameof(ClientSettings));

builder.Services.AddControllers(options => options.Filters.Add<LogAsyncResourceFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutomapperConfig));
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddTypedClient(builder.Configuration);


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
        .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(5)).RepeatForever())
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
