using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Holders
{
    //public static class Claims
    //{
    //    public static IEnumerable<Claim> Get()
    //    {
    //        return new List<Claim>()
    //        {
    //            //Product
    //            new Claim("Add Product", "False"),
    //            new Claim("Edit Product", "False"),
    //            new Claim("Delete Product", "False"),
    //            new Claim("Index Product", "False"),
    //            new Claim("Product List", "False"),
    //            new Claim("Manage Products", "False"),

    //            //Role
    //            new Claim("Create Role", "False"),
    //            new Claim("Edit Role", "False"),
    //            new Claim("Delete Role", "False"),
    //            new Claim("Roles List", "False"),

    //            //User
    //            new Claim("Edit User", "False"),
    //            new Claim("Delete User", "False"),
    //            new Claim("Search Account", "False"),
    //            new Claim("Edit Account Roles", "False"),

    //            //User - Role
    //            new Claim("Add User Role", "False"),
    //            new Claim("Delete User Role", "False"),

    //            //Role - Claim
    //            new Claim("Edit Role Claims", "False"), //Included add & edit
    //        };
    //    }
    //}

    public static class Claims
    {
        //Product
        public static readonly string Add_Product = "Add Product";
        public static readonly string Edit_Product = "Edit Product";
        public static readonly string Delete_Product = "Delete Product";
        public static readonly string Product_Lists = "Products List";

        //Role
        public static readonly string Create_Role = "Create Role";
        public static readonly string Edit_Role = "Edit Role";
        public static readonly string Delete_Role = "Delete Role";
        public static readonly string Roles_List = "Roles List";

        //User
        public static readonly string Edit_User = "Edit User";
        public static readonly string Delete_User = "Delete User";
        public static readonly string Search_Account = "Search Account";
        public static readonly string Edit_Account_Roles = "Edit Account Roles";

        //User - Role
        public static readonly string Add_User_Role = "Add User Role";
        public static readonly string Delete_User_Role = "Delete User Role";

        //Role - Claim
        public static readonly string Edit_Role_Claims = "Edit Role Claims"; //Included add & edit

        //MetaTag
        public static readonly string Edit_ViewPages_MetagTags = "Edit ViewPages MetaTags";

        //Category
        public static readonly string Categories_List = "Categories List";
        public static readonly string Add_Category = "Add Category";
        public static readonly string Edit_Category = "Edit Category";
        public static readonly string Delete_Category = "Delete Category";


        public static IEnumerable<Claim> GetClaims()
        {
            var props = typeof(Claims).GetFields();
            List<Claim> claims = new List<Claim>();

            foreach (var p in props)
            {
                string name = p.GetValue(null).ToString();

                claims.Add(new Claim(name, name));
            }

            return claims;
        }
    }
}
