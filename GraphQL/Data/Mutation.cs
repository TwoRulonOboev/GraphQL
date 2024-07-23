using GraphQL.Data.Repository;
using GraphQL.Model;

namespace GraphQL.Data
{
    namespace GraphQL.Data
    {
        public class Mutation
        {
            private readonly IRepository<Album> _albumRepository;
            private readonly IRepository<Photo> _photoRepository;

            public Mutation(IRepository<Album> albumRepository, IRepository<Photo> photoRepository)
            {
                _albumRepository = albumRepository;
                _photoRepository = photoRepository;
            }

            public async Task<Album> AddAlbumAsync(Album album)
            {
                await _albumRepository.AddAsync(album);
                return album;
            }

            public async Task<Photo> AddPhotoAsync(Photo photo)
            {
                await _photoRepository.AddAsync(photo);
                return photo;
            }

            public async Task<Album> UpdateAlbumAsync(Album album)
            {
                await _albumRepository.UpdateAsync(album);
                return album;
            }

            public async Task<Photo> UpdatePhotoAsync(Photo photo)
            {
                await _photoRepository.UpdateAsync(photo);
                return photo;
            }

            public async Task<bool> DeleteAlbumAsync(int id)
            {
                await _albumRepository.DeleteAsync(id);
                return true;
            }

            public async Task<bool> DeletePhotoAsync(int id)
            {
                await _photoRepository.DeleteAsync(id);
                return true;
            }
        }
    }

}
