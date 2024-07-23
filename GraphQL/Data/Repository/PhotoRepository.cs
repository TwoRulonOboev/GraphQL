using GraphQL.Model;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Repository
{
    public class PhotoRepository : IRepository<Photo>
    {
        private readonly IDbContextFactory _dbContextFactory;

        public PhotoRepository(IDbContextFactory dbContextFactory)
            => _dbContextFactory = dbContextFactory;

        public void Add(Photo photo)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                if (context.Photos.Count(a => a.Id == photo.Id) != 0) return;

                context.Photos.Add(photo);
                context.SaveChanges();
            }
        }

        public void Add(IEnumerable<Photo> photos)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                foreach (Photo photo in photos)
                {
                    if (context.Photos.Count(a => a.Id == photo.Id) != 0) continue;
                    context.Photos.Add(photo);
                }
                context.SaveChanges();
            }
        }

        public async Task AddAsync(Photo photo)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                if (context.Photos.Count(a => a.Id == photo.Id) != 0) return;

                await context.Photos.AddAsync(photo);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(IEnumerable<Photo> photos)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                foreach (Photo photo in photos)
                {
                    if (context.Photos.Count(a => a.Id == photo.Id) != 0) continue;
                    await context.Photos.AddAsync(photo);
                }
                await context.SaveChangesAsync();
            }
        }


        public Photo Get(int id)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Photos.SingleOrDefault(a => a.Id == id);
            }
        }

        public List<Photo> Get()
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Photos.ToList();
            }
        }

        public List<Photo> Get(Func<Photo, bool> filter)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Photos.Where(filter).ToList();
            }
        }

        public async Task<Photo> GetAsync(int id)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return await context.Photos.SingleOrDefaultAsync(a => a.Id == id);
            }
        }

        public async Task<List<Photo>> GetAsync()
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return await context.Photos.ToListAsync();
            }
        }

        public void Update(Photo entity)
        {
            using (var context = _dbContextFactory.Create())
            {
                Photo? temp = context.Photos.SingleOrDefault(a => a.Id == entity.Id);

                if (temp == null) return;

                temp.AlbumId = entity.AlbumId;
                temp.Title = entity.Title;
                temp.Url = entity.Url;
                temp.ThumbnailUrl = entity.ThumbnailUrl;
                temp.Album = entity.Album;

                context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Photo entity)
        {
            using (var context = _dbContextFactory.Create())
            {
                Photo? temp = await context.Photos.SingleOrDefaultAsync(a => a.Id == entity.Id);

                if (temp == null) return;

                temp.AlbumId = entity.AlbumId;
                temp.Title = entity.Title;
                temp.Url = entity.Url;
                temp.ThumbnailUrl = entity.ThumbnailUrl;
                temp.Album = entity.Album;

                await context.SaveChangesAsync();
            }
        }


        public void Delete(int id)
        {
            Photo album = new Photo { Id = id };

            using (var context = _dbContextFactory.Create())
            {
                context.Photos.Remove(album);

                context.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Photo album = new Photo { Id = id };

            using (var context = _dbContextFactory.Create())
            {
                context.Photos.Remove(album);

                await context.SaveChangesAsync();
            }
        }
    }
}
