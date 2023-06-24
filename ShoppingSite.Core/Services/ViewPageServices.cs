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
    public class ViewPageServices : IViewPage
    {
        protected DContext context;

        public ViewPageServices(DContext context)
        {
            this.context = context;
        }

        public ValueTask<EntityEntry<ViewPage>> AddAsync(ViewPage page)
        {
            return context.ViewPages.AddAsync(page);
        }

        public Task<ViewPage?> FindAsync(string url)
        {
            return context.ViewPages.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Url == url);
        }

        public void Remove(ViewPage page)
        {
            context.ViewPages.Remove(page);
        }

        public async Task Remove(string url)
        {
            Remove(await FindAsync(url));
        }

        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(ViewPage page)
        {
            context.ViewPages.Update(page);
        }
    }
}
