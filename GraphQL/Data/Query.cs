using GraphQL.Data.Repository;
using GraphQL.Model;

namespace GraphQL.Data
{
    public class Query
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Photo> _photoRepository;

        public Query(IRepository<Album> albumRepository, IRepository<Photo> photoRepository)
        {
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
        }

        public async Task<List<Album>> GetAlbumsAsync() => await _albumRepository.GetAsync();

        public async Task<Album> GetAlbum(int id) => await _albumRepository.GetAsync(id);

        public async Task<List<Photo>> GetPhotos() => await _photoRepository.GetAsync();

        public async Task<Photo> GetPhoto(int id) => await _photoRepository.GetAsync(id);
    }
}
