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
        Task<Category> FindAsync(Guid id);
        Task<List<Category>> SearchAsync(string query);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task SaveAsync();
    }
}
