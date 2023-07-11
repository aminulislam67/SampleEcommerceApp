using Ecommerce.Database;
using Ecommerce.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class ProductRepository
    {
        ApplicationDbContext _db;

        public ProductRepository()
        {
            _db = new ApplicationDbContext();
        }

        public bool Add(Product Product)
        {
            _db.Products.Add(Product);




            return _db.SaveChanges() > 0;
        }

        public bool AddRange(ICollection<Product> Products)
        {
            _db.Products.AddRange(Products);
            return _db.SaveChanges() > 0;
        }

        public bool Update(Product Product)
        {
            _db.Products.Update(Product);

            return _db.SaveChanges() > 0;
        }

        public bool UpdateRange(ICollection<Product> Products)
        {
            _db.Products.UpdateRange(Products);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(Product Product)
        {
            _db.Products.Remove(Product);
            return _db.SaveChanges() > 0;
        }

        public Product GetById(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public ICollection<Product> GetAll()
        {
            return _db.Products.ToList();
        }
    }
}
