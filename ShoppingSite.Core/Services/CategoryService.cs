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
    public class CategoryService : ICategory
    {
        private readonly DContext context;

        public CategoryService(DContext context)
        {
            this.context = context;
        }

        public ValueTask<EntityEntry<Category>> AddAsync(Category category)
        {
            return context.Categories.AddAsync(category);
        }

        public void Delete(int id)
        {
            //Test it
            context.Categories.Remove(new Category { Id = id });
        }

        public Task<Category?> FindAsync(int id)
        {
            return context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Category>> FindAsync(List<string> names)
        {
            return context.Categories.Where(x => names.Any(a => x.Name == a)).ToListAsync();
        }

        public Task<List<Category>> GetAll()
        {
            return context.Categories.ToListAsync();
        }

        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task<List<Category>> SearchAsync(string query)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Category category)
        {
            context.Categories.Update(category);
        }
    }
}
