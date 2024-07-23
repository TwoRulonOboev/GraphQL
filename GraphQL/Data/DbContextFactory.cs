namespace GraphQL.Data
{
    public interface IDbContextFactory
    {
        MyDbContext Create();
    }
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _config;

        public DbContextFactory(IConfiguration config) => _config = config;

        public MyDbContext Create() => new MyDbContext(_config);
    }
}
