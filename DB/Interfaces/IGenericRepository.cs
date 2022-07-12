using System.Linq.Expressions;

namespace DB.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        public Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        public void AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        public void AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        T FindFirstorDefault(Expression<Func<T, bool>> expression);
    }
}
