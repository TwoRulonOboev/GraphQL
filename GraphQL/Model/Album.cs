namespace GraphQL.Model
{
    public class Album
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<Photo> Photos { get; set; } = null!;
    }
}