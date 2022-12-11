using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingSite.Core.ViewModels.Role;
using ShoppingSite.Core.ViewModels.User;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public class UserRolesModerator : Controller
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public ModelStateDictionary ModelState { get; }

        public UserRolesModerator(ref UserManager<User> userManager, ref RoleManager<IdentityRole> roleManager, ModelStateDictionary modelState)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            ModelState = modelState;
        }

        public async Task<IActionResult> FindRoles(string userId)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await UserManager.FindByIdAsync(userId);
            var userRoles = await UserManager.GetRolesAsync(user);

            var viewModel = new UserRolesViewModel();
            viewModel.Id = userId;

            foreach (var role in RoleManager.Roles)
            {
                viewModel.Roles.Add(new()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Included = userRoles.Contains(role.Name)
                });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditRoles(UserRolesViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await UserManager.FindByIdAsync(viewModel.Id);

            foreach (var role in viewModel.Roles)
            {
                if (role.Included)
                    await UserManager.AddToRoleAsync(user, role.Name);
                else
                    await UserManager.RemoveFromRoleAsync(user, role.Name);
            }

            return RedirectToAction("Edit", routeValues: viewModel.Id);
        }
    }
}
