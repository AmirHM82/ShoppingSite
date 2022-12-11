using ShoppingSite.Core.Interfaces;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Services
{
    public class CategoryService : ICategory
    {
        public Task<Category> AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> SearchAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
