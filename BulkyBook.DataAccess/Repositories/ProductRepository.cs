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

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Product product)
        {
            var updatedProduct = _db.Products.Find(product.Id);
            if (updatedProduct != null)
            {
                updatedProduct.Name = product.Name;
                updatedProduct.Description = product.Description;
                updatedProduct.Price = product.Price;
                updatedProduct.ISBN = product.ISBN;
                updatedProduct.Author = product.Author;
                updatedProduct.Price50 = product.Price50;
                updatedProduct.Price100 = product.Price100;
                updatedProduct.CoverTypeId = product.CoverTypeId;
                updatedProduct.CategoryId = product.CategoryId;

                if (product.ImageUrl != null)
                {
                    updatedProduct.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
