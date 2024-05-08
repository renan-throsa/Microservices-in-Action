using LoyaltyProgram.Data;
using LoyaltyProgram.Service;
using Quartz;
using Quartz.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1 - Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEventStore, EventStore>();
builder.Services.AddTransient<ILoyaltyProgramUserStore, LoyaltyProgramUserStore>();
builder.Services.AddHttpClient<ISpecialOffersClient, SpecialOffersClient>();

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
        .StartNow()
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
