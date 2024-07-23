using GraphQL.Pars.Model;
using System.Text.Json;

class Pars
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");

        var albumsResponse = await client.GetAsync("albums");
        albumsResponse.EnsureSuccessStatusCode();
        var albumsJson = await albumsResponse.Content.ReadAsStringAsync();
        var albums = JsonSerializer.Deserialize<Album[]>(albumsJson);

        var photosResponse = await client.GetAsync("photos");
        photosResponse.EnsureSuccessStatusCode();
        var photosJson = await photosResponse.Content.ReadAsStringAsync();
        var photos = JsonSerializer.Deserialize<Photo[]>(photosJson);

        using var dbContext = new MyDbContext();
        dbContext.Database.EnsureCreated(); 

        dbContext.Albums.AddRange(albums);
        dbContext.Photos.AddRange(photos);
        await dbContext.SaveChangesAsync();
    }
}