using GraphQL.Data;
using GraphQL.Data.Repository;
using GraphQL.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("./config.json")
    .Build();

builder.Services.AddSingleton<HttpClient>()
    .AddSingleton(config)
    .AddTransient<IDbContextFactory, DbContextFactory>()
    .AddTransient<IRepository<Album>, AlbumRepository>()
    .AddTransient<IRepository<Photo>, PhotoRepository>()
    .AddTransient<DataLoader>();


var app = builder.Build();

DataLoader dataLoader = app.Services.GetService<DataLoader>()!;

await dataLoader.LoadDataAsync();

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
