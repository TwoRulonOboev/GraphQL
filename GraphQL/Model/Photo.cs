using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQL.Model
{
    public class Photo
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;

        public virtual Album Album { get; set; } = null!;
    }
}