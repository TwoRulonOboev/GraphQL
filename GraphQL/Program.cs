using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using GraphQL.Data;
using GraphQL.Model;
using GraphQL.Data.GraphQL.Data;
using GraphQL.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Добавление служб в контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

// Настройка GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

//// Инициализация данных
//DataLoader dataLoader = app.Services.GetService<DataLoader>()!;
//await dataLoader.LoadDataAsync();

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
