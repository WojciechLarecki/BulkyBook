using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace BulkyBook.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly ApplicationDbContext _db;
        readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IEnumerable<T> GetAll(string? includeProperites = null)
        {
            IQueryable<T> query = _dbSet;
            if (includeProperites != null)
            {
                var properties = includeProperites.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property.Trim());
                }
            }

            return query.ToList();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperites = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (includeProperites  != null)
            {
                var properties = includeProperites.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property.Trim());
                }
            }

            return query.FirstOrDefault(filter);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
