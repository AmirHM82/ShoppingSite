using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Holders
{
    public static class Claims
    {
        public static IEnumerable<Claim> Get()
        {
            return new List<Claim>()
            {
                //Product
                new Claim("Add Product", "False"),
                new Claim("Edit Product", "False"),
                new Claim("Delete Product", "False"),
                new Claim("Index Product", "False"),
                new Claim("Product List", "False"),
                new Claim("Manage Products", "False"),

                //Role
                new Claim("Create Role", "False"),
                new Claim("Edit Role", "False"),
                new Claim("Delete Role", "False"),
                new Claim("Roles List", "False"),

                //User
                new Claim("Edit User", "False"),
                new Claim("Delete User", "False"),
                new Claim("Search Account", "False"),
                new Claim("Edit Account Roles", "False"),

                //User - Role
                new Claim("Add User Role", "False"),
                new Claim("Delete User Role", "False"),

                //Role - Claim
                new Claim("Edit Role Claims", "False"), //Included add & edit
            };
        }
    }
}
