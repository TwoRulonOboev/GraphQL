using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Data;
using GraphQL.Model;
using GraphQL.Data.GraphQL.Data;
using GraphQL.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Добавление служб в контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настройка конфигурации
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("./config.json")
    .Build();

builder.Services.AddSingleton<HttpClient>()
    .AddSingleton(config)
    .AddTransient<IDbContextFactory, DbContextFactory>()
    .AddTransient<IRepository<Album>, AlbumRepository>()
    .AddTransient<IRepository<Photo>, PhotoRepository>()
    .AddTransient<DataLoader>();

<<<<<<< HEAD
// Настройка GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

=======
>>>>>>> 20ed2078e850db47ef1c99f60257656f4fac1b82
var app = builder.Build();

// Инициализация данных
DataLoader dataLoader = app.Services.GetService<DataLoader>()!;
await dataLoader.LoadDataAsync();

// Настройка конвейера HTTP-запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Настройка конечной точки GraphQL и GraphiQL
app.MapGraphQL("/api/graphql");
app.UsePlayground(new PlaygroundOptions
{
    Path = "/api/graphiql",
    QueryPath = "/api/graphql"
});

app.Run();
