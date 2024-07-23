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

        public List<Album> GetAlbums() => _albumRepository.Get();

        public Album GetAlbum(int id) => _albumRepository.Get(id);

        public List<Photo> GetPhotos() => _photoRepository.Get();

        public Photo GetPhoto(int id) => _photoRepository.Get(id);
    }
}
