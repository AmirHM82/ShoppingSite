using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Interfaces
{
    public interface ISubCategory
    {
        Task<SubCategory> FindAsync(Guid id);
        Task<List<SubCategory>> SearchAsync(string query);
        Task<SubCategory> AddAsync(SubCategory SubCategory);
        Task<SubCategory> UpdateAsync(SubCategory SubCategory);
        Task SaveAsync();
    }
}
