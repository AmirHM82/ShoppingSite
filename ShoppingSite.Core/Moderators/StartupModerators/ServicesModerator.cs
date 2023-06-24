using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ShoppingSite.Core.Accessibility.Handlers.Account;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.Middlewares;
using ShoppingSite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators.StartupModerators
{
    public static class ServicesModerator
    {
        public static void AddDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<IProduct, ProductService>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<ISubCategory, SubCategoryService>();
            services.AddScoped<IMetaTag, MetaTagService>();
            services.AddScoped<IViewPage, ViewPageServices>();
            services.AddScoped<MetaTagsLoader>();
        }

        public static void AddPolicyServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, EditAccountHandler>();
        }
    }
}
