using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repositories.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
