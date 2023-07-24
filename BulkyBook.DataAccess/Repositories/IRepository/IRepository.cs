using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        IEnumerable<T> GetAll(string? includeProperites = null);
        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperites = null);
    }
}
