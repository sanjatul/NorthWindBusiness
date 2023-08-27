using System.Linq.Expressions;

namespace NorthWind.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get();
        void Add(T entity);
        void Remove(T entity);
    }
}
