using Polly;
using ShoppingCart.Data;
using ShoppingCart.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
                        .AddTransientHttpErrorPolicy(p =>
                            p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)))

                        );

builder.Services.AddTransient<IShoppingCartStore, ShoppingCartStore>();
builder.Services.AddTransient<IEventStore, EventStore>();
builder.Services.AddTransient<IProductCatalogClient, ProductCatalogClient>();


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
