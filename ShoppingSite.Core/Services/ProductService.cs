using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.DAL.Context;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Services
{
    public class ProductService : IProduct
    {
        protected DContext context;

        public ProductService(DContext context)
        {
            this.context = context;
        }

        public ValueTask<EntityEntry<Product>> AddAsync(Product product)
        {
            return context.Products.AddAsync(product);
        }

        public async Task<int> CountPages()
        {
            var totalItems = await CountProducts();
            return (int)Math.Ceiling((double)totalItems / 10);
        }

        public Task<int> CountProducts()
        {
            return context.Products.CountAsync();
        }

        public Task<Product> FindAsync(int id)
        {
            return context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAsync(int page)
        {
            return await context.Products.Skip(--page * 10).Take(10).ToListAsync();
        }

        public void Remove(Product product)
        {
            context.Products.Remove(product);
        }

        public async Task<Product> Remove(int id)
        {
            var product = await FindAsync(id);
            context.Products.Remove(product);
            return product;
        }

        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task<List<Product>> SearchAsync(string query)
        {
            return context.Products.Where(x=>x.Title.Contains(query)).ToListAsync();
        }

        public void Update(Product product)
        {
            if (product == null) return;

            context.Attach(product);
            context.Products.Update(product);
        }
    }
}
