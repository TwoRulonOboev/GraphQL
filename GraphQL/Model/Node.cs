namespace GraphQL.Model
{
    public class Node
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Data { get; set; } = null!;
        public int Index { get; set; }

        public List<Node> Children { get; set;} = new List<Node>();
        public Node? Parent { get; set; }
    }
}