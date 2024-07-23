namespace GraphQL.Data.Repository
{
    public interface IRepository<T>
    {
        // Синхронные методы
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        void Update(T entity);
        T Get(int id);
        List<T> Get();
        List<T> Get(Func<T, bool> filter);
        void Delete(int id);


        //Асинхронные методы
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<List<T>> GetAsync();
        Task DeleteAsync(int id);
    }
}
