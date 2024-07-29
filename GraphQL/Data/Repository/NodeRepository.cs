using GraphQL.Model;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Repository
{
    public class NodeRepository : IRepository<Node>
    {
        private readonly IDbContextFactory _dbContextFactory;

        public NodeRepository(IDbContextFactory dbContextFactory)
            => _dbContextFactory = dbContextFactory;

        public void Add(Node node)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                if (context.Nodes.Count(a => a.Id == node.Id) != 0) return;

                context.Nodes.Add(node);
                context.SaveChanges();
            }
        }

        public void Add(IEnumerable<Node> nodes)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                foreach (Node node in nodes)
                {
                    if (context.Nodes.Count(a => a.Id == node.Id) != 0) continue;
                    context.Nodes.Add(node);
                }
                context.SaveChanges();
            }
        }

        public async Task AddAsync(Node node)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                if (context.Nodes.Count(a => a.Id == node.Id) != 0) return;

                await context.Nodes.AddAsync(node);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(IEnumerable<Node> nodes)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                foreach (Node node in nodes)
                {
                    if (context.Nodes.Count(a => a.Id == node.Id) != 0) continue;
                    await context.Nodes.AddAsync(node);
                }
                await context.SaveChangesAsync();
            }
        }


        public Node? Get(int id)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Nodes.Select(p => new Node
                {
                    Id          = p.Id,
                    ParentId    = p.ParentId,
                    Data        = p.Data,
                    Index       = p.Index,

                    Children    = p.Children,
                    Parent      = p.Parent
                }).SingleOrDefault(a => a.Id == id);
            }
        }

        public List<Node> Get()
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Nodes.Select(p => new Node
                {
                    Id          = p.Id,
                    ParentId    = p.ParentId,
                    Data        = p.Data,
                    Index       = p.Index,

                    Children    = p.Children,
                    Parent      = p.Parent
                }).ToList();
            }
        }

        public List<Node> Get(Func<Node, bool> filter)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return context.Nodes.Select(p => new Node
                {
                    Id          = p.Id,
                    ParentId    = p.ParentId,
                    Data        = p.Data,
                    Index       = p.Index,

                    Children    = p.Children,
                    Parent      = p.Parent
                }).Where(filter).ToList();
            }
        }

        public async Task<Node?> GetAsync(int id)
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return await context.Nodes.Select(p => new Node
                {
                    Id          = p.Id,
                    ParentId    = p.ParentId,
                    Data        = p.Data,
                    Index       = p.Index,

                    Children    = p.Children,
                    Parent      = p.Parent
                }).SingleOrDefaultAsync(a => a.Id == id);
            }
        }

        public async Task<List<Node>> GetAsync()
        {
            using (MyDbContext context = _dbContextFactory.Create())
            {
                return await context.Nodes.Select(p => new Node
                {
                    Id          = p.Id,
                    ParentId    = p.ParentId,
                    Data        = p.Data,
                    Index       = p.Index,

                    Children    = p.Children,
                    Parent      = p.Parent
                }).ToListAsync();
            }
        }

        public void Update(Node node)
        {
            using (var context = _dbContextFactory.Create())
            {
                Node? temp = context.Nodes.SingleOrDefault(a => a.Id == node.Id);

                if (temp == null) return;

                temp.ParentId   = node.ParentId;
                temp.Data       = node.Data;
                temp.Index      = node.Index;

                context.SaveChanges();
            }
        }

        public async Task UpdateAsync(Node node)
        {
            using (var context = _dbContextFactory.Create())
            {
                Node? temp = await context.Nodes.SingleOrDefaultAsync(a => a.Id == node.Id);

                if (temp == null) return;

                temp.ParentId   = node.ParentId;
                temp.Data       = node.Data;
                temp.Index      = node.Index;

                await context.SaveChangesAsync();
            }
        }


        public void Delete(int id)
        {
            Node album = new Node { Id = id };

            using (var context = _dbContextFactory.Create())
            {
                context.Nodes.Remove(album);

                context.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Node album = new Node { Id = id };

            using (var context = _dbContextFactory.Create())
            {
                context.Nodes.Remove(album);

                await context.SaveChangesAsync();
            }
        }
    }
}
