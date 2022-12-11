using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShoppingSite.Core.Accessibility.Requirements.Account;
using ShoppingSite.DAL.Context;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators.StartupModerators
{
    public static class IdentitiesModerator
    {
        public static void AddPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy("EditRoleClaimsPolicy", policy => policy.RequireClaim("Edit Role Claims"));
            options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
            options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
            options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role"));
            options.AddPolicy("RolesListPolicy", policy => policy.RequireClaim("Roles List"));

            options.AddPolicy("EditAccountRolesPolicy", policy => policy.RequireClaim("Edit Account Roles"));

            options.AddPolicy("SearchAccountPolicy", policy => policy.RequireClaim("Search Account"));

            options.AddPolicy("EditAccountPolicy", policy => policy.AddRequirements(new ManageAccountRequirement()));

            options.AddPolicy("AddProductPolicy", policy => policy.RequireClaim("Add Product"));
            options.AddPolicy("EditProductPolicy", policy => policy.RequireClaim("Edit Product"));
            options.AddPolicy("DeleteProductPolicy", policy => policy.RequireClaim("Delete Product"));
            options.AddPolicy("IndexProductPolicy", policy => policy.RequireClaim("Index Product"));
            options.AddPolicy("ProductListPolicy", policy => policy.RequireClaim("Product List"));
            options.AddPolicy("ManageProductsPolicy", policy => policy.RequireClaim("Manage Products"));

            /*
             * We need lots of policies here:
             * 
             * EditClaims {AddClaim (to role) - RemoveClaim (from role)}
             * 
             * CreateRole
             * EditRole
             * DeleteRole
             * 
             * EditAccount (Already have)
             * DeleteAccount
             * 
             * AddProduct
             * EditProduct
             * DeleteProduct
             */

            //This makes Authorization stop proccessing other handleres when one of them fails
            //options.InvokeHandlersAfterFailure = false;
        }

        public static void AddCustomeIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.AddIdentityOptions();
            }).AddEntityFrameworkStores<DContext>();
        }

        public static void AddIdentityOptions(this IdentityOptions options)
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.SignIn.RequireConfirmedPhoneNumber = true;
        }
    }
}
