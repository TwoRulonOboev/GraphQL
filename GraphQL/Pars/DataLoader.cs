using GraphQL.Data.Repository;
using GraphQL.Model;

class DataLoader
{
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public IRepository<Album> AlbumsRepository { get; set; }
    public IRepository<Photo> PhotosRepository { get; set; }

    public DataLoader(IRepository<Album> albums, IRepository<Photo> photos, IConfiguration config, HttpClient client)
    {
        AlbumsRepository = albums;
        PhotosRepository = photos;
        _config = config;
        _client = client;
    }

    public async Task LoadDataAsync()
    {
        List<Album>? albums = await _client.GetFromJsonAsync<List<Album>>(_config["DownloadUrls:Albums"]);
        List<Photo>? photos = await _client.GetFromJsonAsync<List<Photo>>(_config["DownloadUrls:Photos"]);

        if(albums != null) await AlbumsRepository.AddAsync(albums);
        if(photos != null) await PhotosRepository.AddAsync(photos);
    }
}