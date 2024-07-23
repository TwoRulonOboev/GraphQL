namespace GraphQL.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;

        public Album Album { get; set; } = null!;
    }
}