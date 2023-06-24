using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Interfaces
{
    public interface IMetaTag
    {
        ValueTask<EntityEntry<MetaTag>> AddAsync(MetaTag tag);
        Task<MetaTag?> FindAsync(int tagId);
        void Update(MetaTag tag);
        Task SaveAsync();
        void Remove(MetaTag tag);
        Task<Product> Remove(int id);
    }
}
