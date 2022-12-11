using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.DAL.Context
{
    //IdentityDbContext instead od DbContext beacuse we gonna use identity stuff (Login, register, ...). This class is inherited from DbContext
    public class DContext : IdentityDbContext<User>
    {
        public DContext(DbContextOptions<DContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*
             * By default the delete proccess is "Cascade". It means that when a father data is deleted, the childs are deleted too.
             * ex: When a role is deleted => The users who have the role are deleted too!
             * 
             * So we change it to Restrict, this way user (Manager) has to remove users from that role first, then delete the role.
             * 
             * Attention: In database the "On Delete" behavior sets to "No Action"
             */
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
