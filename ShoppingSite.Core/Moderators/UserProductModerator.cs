using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public class UserProductModerator : Controller
    {
        protected IProduct productService;
        protected UserManager<User> userManager;

        public UserProductModerator(IProduct productService, UserManager<User> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddToCart(int productId, int page)
        {
            var product = await productService.FindAsync(productId);
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            user.Cart.Add(product);

            await userManager.UpdateAsync(user);

            return RedirectToAction("List", "Product", new { page = page });
        }
    }
}
