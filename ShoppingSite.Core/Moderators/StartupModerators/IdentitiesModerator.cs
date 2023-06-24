using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShoppingSite.Core.Accessibility.Requirements.Account;
using ShoppingSite.Core.Holders;
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
            options.AddPolicy("EditRoleClaimsPolicy", policy => policy.RequireClaim(Claims.Edit_Role_Claims));
            options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim(Claims.Create_Role));
            options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim(Claims.Delete_Role));
            options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim(Claims.Edit_Role));
            options.AddPolicy("RolesListPolicy", policy => policy.RequireClaim(Claims.Roles_List));

            options.AddPolicy("EditAccountRolesPolicy", policy => policy.RequireClaim(Claims.Edit_Account_Roles));

            options.AddPolicy("SearchAccountPolicy", policy => policy.RequireClaim(Claims.Search_Account));

            options.AddPolicy("EditAccountPolicy", policy => policy.AddRequirements(new ManageAccountRequirement())); //I have to work on it (Admins with related claim and the user it self can edit)

            options.AddPolicy("AddProductPolicy", policy => policy.RequireClaim(Claims.Add_Product));
            options.AddPolicy("EditProductPolicy", policy => policy.RequireClaim(Claims.Edit_Product));
            options.AddPolicy("DeleteProductPolicy", policy => policy.RequireClaim(Claims.Delete_Product));

            //They are gonna be public (Visitable without account
            //options.AddPolicy("IndexProductPolicy", policy => policy.RequireClaim(Claims.Index_Product));
            //options.AddPolicy("ProductListPolicy", policy => policy.RequireClaim(Claims.Product_List));
            //options.AddPolicy("ManageProductsPolicy", policy => policy.RequireClaim(Claims.Manage_Products));

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
