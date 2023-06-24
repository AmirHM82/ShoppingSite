using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Interfaces
{
    public interface ICategory
    {
        Task<Category> FindAsync(int id);
        Task<List<Category>> FindAsync(List<string> names);
        Task<List<Category>> SearchAsync(string query);
        Task<List<Category>> GetAll();
        ValueTask<EntityEntry<Category>> AddAsync(Category category);
        void UpdateAsync(Category category);
        void Delete(int id);
        Task SaveAsync();
    }
}
