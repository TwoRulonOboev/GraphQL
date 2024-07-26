using GraphQL.Model;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Node> Nodes { get; set; }

    private readonly string? _connectionString;

    public MyDbContext(IConfiguration config)
    {
        _connectionString = config["PostgresConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
}
