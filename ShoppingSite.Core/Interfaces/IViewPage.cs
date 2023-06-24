using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Interfaces
{
    public interface IViewPage
    {
        Task<ViewPage?> FindAsync(string url);
        ValueTask<EntityEntry<ViewPage>> AddAsync(ViewPage page);
        void Update(ViewPage page);
        Task SaveAsync();
        void Remove(ViewPage page);
        Task Remove(string url);
    }
}
