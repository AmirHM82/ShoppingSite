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
    public class MetaTagService : IMetaTag
    {
        protected DContext context;

        public MetaTagService(DContext context)
        {
            this.context = context;
        }

        public ValueTask<EntityEntry<MetaTag>> AddAsync(MetaTag tag)
        {
            return context.MetaTags.AddAsync(tag);
        }

        public Task<MetaTag?> FindAsync(int tagId)
        {
            return context.MetaTags.FirstOrDefaultAsync(x => x.Id == tagId);
        }

        public void Remove(MetaTag tag)
        {
            context.MetaTags.Remove(tag);
        }

        public Task<Product> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(MetaTag tag)
        {
            context.Update(tag);
        }
    }
}
