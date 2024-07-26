using GraphQL.Data.Repository;
using GraphQL.Model;

namespace GraphQL.Data
{
    public interface ITreeManager
    {
        Node GetRootOfTree(int id);
        Task<Node> GetRootOfTreeAsync(int id);
    }

    public class TreeManager : ITreeManager
    {
        private readonly IRepository<Node> _nodeRepository;

        public TreeManager(IRepository<Node> nodeRepository)
            => _nodeRepository = nodeRepository;

        public Node GetRootOfTree(int id)
        {
            Node root = _nodeRepository.Get(id);

            SetChildren(root);

            return root;
        }

        private void SetChildren(Node Parent)
        {
            foreach (Node node in Parent.Children)
            {
                node.Parent = Parent;
                node.Children = _nodeRepository.Get(node.Id).Children;

                SetChildren(node);
            }
        }

        public async Task<Node> GetRootOfTreeAsync(int id)
        {
            Node root = _nodeRepository.Get(id);

            await SetChildrenAsync(root);

            return root;
        }

        private async Task SetChildrenAsync(Node Parent)
        {
            foreach (Node node in Parent.Children)
            {
                node.Parent = Parent;
                node.Children = (await _nodeRepository.GetAsync(node.Id)).Children;

                await SetChildrenAsync(node);
            }
        }
    }
}
