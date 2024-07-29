using GraphQL.Data.Repository;
using GraphQL.Model;
using System.Text.RegularExpressions;

namespace GraphQL.Data
{
    public class Query
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Photo> _photoRepository;
        private readonly ITreeManager _treeManager;
        private readonly IDataLoader _dataLoader;

        public Query(IRepository<Album> albumRepository, IRepository<Photo> photoRepository, ITreeManager treeManager, IDataLoader dataLoader)
        {
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
            _treeManager = treeManager;
            _dataLoader = dataLoader;
        }

        public async Task<List<Album>> GetAlbumsAsync() => await _albumRepository.GetAsync();

        public async Task<Album> GetAlbum(int id) => await _albumRepository.GetAsync(id);

        public async Task<List<Photo>> GetPhotos() => await _photoRepository.GetAsync();

        public async Task<Photo> GetPhoto(int id) => await _photoRepository.GetAsync(id);


        public async Task<List<Node>> GetTree(int id)
        {
            Node root = await _treeManager.GetRootOfTreeAsync(id);
            List<Node> nodes = _treeManager.ToList(root);

            Regex regex = new Regex("^http(s)?://.+[.].+");

            for (int i = 0; i < nodes.Count-1; i++)
            {
                if (!regex.IsMatch(nodes[i].Data)) continue;

                nodes[i] = new Node
                {
                    Id          = nodes[i].Id,
                    ParentId    = nodes[i].ParentId,
                    Data        = await _dataLoader.GetStringAsync(nodes[i].Data),
                    Index       = nodes[i].Index
                };
            }

            return nodes;
        }
    }
}
