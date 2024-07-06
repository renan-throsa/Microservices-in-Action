using Microsoft.Net.Http.Headers;
using Polly;
using ProductCatalog.Utils;
using ShoppingCart.Data;
using ShoppingCart.Service;
using ShoppingCart.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var clientSettingsSection = builder.Configuration.GetSection(nameof(ClientSettings));
var dataBaseSettingsSection = builder.Configuration.GetSection(nameof(DataBaseSettings));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>((HttpClient client) =>
{
    string address = clientSettingsSection.Get<ClientSettings>().Route; client.BaseAddress = new Uri(address); client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)))
);



builder.Services.Configure<DataBaseSettings>(dataBaseSettingsSection);
builder.Services.AddTransient<IShoppingCartStore, ShoppingCartStore>();
builder.Services.AddTransient<IEventStore, EventStore>();
builder.Services.AddTransient<IProductCatalogClient, ProductCatalogClient>();
builder.Services.AddSingleton<ApplicationContext>();

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
