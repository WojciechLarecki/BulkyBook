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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(CoverType coverType)
        {
            _db.Update(coverType);
        }
    }
}
