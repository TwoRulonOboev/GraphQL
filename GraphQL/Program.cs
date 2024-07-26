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
    .AddTransient<IRepository<Node>, NodeRepository>()
    .AddTransient<ITreeManager, TreeManager>()
    .AddTransient<DataLoader>();

// Настройка GraphQL
builder.Services
    .AddGraphQLServer()
    .AddErrorFilter<OurErrorFilter>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

//DataLoader dataLoader = app.Services.GetService<DataLoader>()!;
//await dataLoader.LoadDataAsync();


// Проверка Мудрого дуба
//Node root = app.Services.GetService<ITreeManager>()!.GetRootOfTree(1);


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
