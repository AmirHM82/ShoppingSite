using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Interfaces
{
    public interface IProduct
    {
        Task<Product> FindAsync(int id);
        Task<List<Product>> SearchAsync(string query);
        Task<List<Product>> GetAsync(int page);
        ValueTask<EntityEntry<Product>> AddAsync(Product product);
        void Update(Product product);
        Task SaveAsync();
        void Remove(Product product);
        Task<Product> Remove(int id);
        Task<int> CountProducts();
        Task<int> CountPages();
    }
}
