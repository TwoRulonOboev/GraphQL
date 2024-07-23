using GraphQL.Pars.Model;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=GraphQLDB;UserId=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>().HasKey(a => a.Id);
        modelBuilder.Entity<Photo>().HasKey(p => p.Id);

        modelBuilder.Entity<Photo>().HasOne(p => p.Album)
            .WithMany(a => a.Photos)
            .HasForeignKey(p => p.AlbumId);
    }
}