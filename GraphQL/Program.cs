using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using GraphQL.Data;
using GraphQL.Model;
using GraphQL.Data.GraphQL.Data;
using GraphQL.Data.Repository;
using GraphQL.Addition;

var builder = WebApplication.CreateBuilder(args);

// Настройка конфигурации
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("./config.json")
    .Build();

// Добавление служб в контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<HttpClient>()
    .AddSingleton(config)
    .AddTransient<IDbContextFactory, DbContextFactory>()
    .AddTransient<IRepository<Album>, AlbumRepository>()
    .AddTransient<IRepository<Photo>, PhotoRepository>()
    .AddTransient<DataLoader>();

builder.Services.AddDbContext<MyDbContext>();

// Настройка GraphQL
builder.Services
    .AddGraphQLServer()
    .AddErrorFilter<OurErrorFilter>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

DataLoader dataLoader = app.Services.GetService<DataLoader>()!;
await dataLoader.LoadDataAsync();

// Обеспечение создания базы данных при запуске
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    dbContext.EnsureDatabaseCreated();
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
