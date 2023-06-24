using ShoppingSite.Core.Interfaces;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Services
{
    public class SubCategoryService : ISubCategory
    {
        public Task<SubCategory> AddAsync(SubCategory SubCategory)
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SubCategory>> SearchAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> UpdateAsync(SubCategory SubCategory)
        {
            throw new NotImplementedException();
        }
    }
}
