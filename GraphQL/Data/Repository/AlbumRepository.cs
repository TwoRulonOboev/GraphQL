using GraphQL.Model;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Repository
{
    public class AlbumRepository : IRepository<Album>
    {
        private readonly IDbContextFactory _dbContextFactory;

        public AlbumRepository(IDbContextFactory dbContextFactory) 
            => _dbContextFactory = dbContextFactory;

        public void Add(Album album)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                if(context.Albums.Count(a => a.Id == album.Id) != 0) return;

                context.Albums.Add(album);
                context.SaveChanges();
            }
        }

        public void Add(IEnumerable<Album> albums)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                foreach(Album album in albums)
                {
                    if (context.Albums.Count(a => a.Id == album.Id) != 0) continue;
                    context.Albums.Add(album);
                }
                context.SaveChanges();
            }
        }

        public async Task AddAsync(Album album)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                if (context.Albums.Count(a => a.Id == album.Id) != 0) return;

                await context.Albums.AddAsync(album);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(IEnumerable<Album> albums)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                foreach (Album album in albums)
                {
                    if (context.Albums.Count(a => a.Id == album.Id) != 0) continue;
                    await context.Albums.AddAsync(album);
                }
                await context.SaveChangesAsync();
            }
        }


        public Album Get(int id)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Albums.SingleOrDefault(a => a.Id == id);
            }
        }

        public List<Album> Get()
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Albums.ToList();
            }
        }

        public List<Album> Get(Func<Album, bool> filter)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Albums.Where(filter).ToList();
            }
        }

        public async Task<Album> GetAsync(int id)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return await context.Albums.SingleOrDefaultAsync(a => a.Id == id);
            }
        }

        public async Task<List<Album>> GetAsync()
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return await context.Albums.ToListAsync();
            }
        }

        public void Update(Album entity)
        {
            using (var context = _dbContextFactory.Create())
            {
                Album? temp = context.Albums.SingleOrDefault(a => a.Id == entity.Id);

                if (temp == null) return;

                temp.Title = entity.Title;
                temp.UserId = entity.UserId;
                temp.Photos = entity.Photos;

                context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Album entity)
        {
            using (var context = _dbContextFactory.Create())
            {
                Album? temp = await context.Albums.SingleOrDefaultAsync(a => a.Id == entity.Id);

                if (temp == null) return;

                temp.Title = entity.Title;
                temp.UserId = entity.UserId;
                temp.Photos = entity.Photos;

                await context.SaveChangesAsync();
            }
        }


        public void Delete(int id)
        {
            Album album = new Album { Id = id };

            using (var context = _dbContextFactory.Create())
            {
                context.Albums.Remove(album);

                context.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Album album = new Album { Id = id };

            using (var context = _dbContextFactory.Create())
            {
                context.Albums.Remove(album);

                await context.SaveChangesAsync();
            }
        }
    }
}
