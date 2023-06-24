using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Core.Holders;
using ShoppingSite.Core.Models.Operation;
using ShoppingSite.Core.ViewModels.Role;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public delegate void RoleModeratorEventHandler(List<Result> results, ref IActionResult returnView);

    public class RoleModerator : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public ModelStateDictionary ModelState { get; }

        public RoleModerator(RoleManager<IdentityRole> roleManager, ModelStateDictionary modelState)
        {
            RoleManager = roleManager;
            ModelState = modelState;
        }

        public event RoleModeratorEventHandler? RecievedResult;

        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return RoleManager.Roles;
            }
        }

        public async Task<IActionResult> CreateAsync(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = roleViewModel.Name
                };

                var result = await RoleManager.CreateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("List", "Role");
            }

            return View(roleViewModel);
        }

        /// <summary>
        /// This is for get requests
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> FindAsync(string id)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(id);
                if (role is not null)
                    return View(new RoleViewModel()
                    {
                        Id = id,
                        Name = role.Name,
                    });
                else
                    return View("NotFound");
            }

            return View();
        }

        /// <summary>
        /// This is for post requests
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditAsync(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(viewModel.Id);
                if (role is null)
                    return View("NotFound");

                role.Name = viewModel.Name;
                var result = await RoleManager.UpdateAsync(role);
                if (!result.Succeeded)
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// This is for post requests
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role is null)
                return View("NotFound");
            try
            {
                var result = await RoleManager.DeleteAsync(role);
                if (!result.Succeeded)
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            catch (DbUpdateException ex)
            {
                ViewBag.ErrorTitle = $"{role.Name} در حال استفاده است";
                ViewBag.ErrorMessage = $"{role.Name} قبل از حذف کردن این نقش نیاز به حذف کاربران از این نقش است";
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> AddClaimsAsync(string roleId, IActionResult? returnView, IEnumerable<ClaimViewModel> claimViewModels)
        {
            Queue<IdentityResult> results = new();
            var role = await RoleManager.FindByIdAsync(roleId);

            foreach (var viewModel in claimViewModels)
            {
                if (viewModel.Exist)
                    results.Enqueue(await RoleManager.AddClaimAsync(role, new(viewModel.ClaimType, viewModel.ClaimType)));
            }

            RecievedResult.Invoke(results.Adapt<List<Result>>(), ref returnView);

            return returnView;
        }

        public async Task<IActionResult> RemoveAllClaimsAsync(string roleId, IActionResult? returnView)
        {
            Queue<IdentityResult> results = new();

            var role = await RoleManager.FindByIdAsync(roleId);

            var claims = await RoleManager.GetClaimsAsync(role);

            foreach (var model in claims)
            {
                results.Enqueue(await RoleManager.RemoveClaimAsync(role, model));
            }

            RecievedResult.Invoke(results.Adapt<List<Result>>(), ref returnView);

            return returnView;
        }

        /// <returns>RoleClaimsViewModel</returns>
        public async Task<IActionResult> GetClaimsAsync(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            List<Claim> roleClaims = new();
            if (role is not null)
                roleClaims.AddRange(await RoleManager.GetClaimsAsync(role));

            var viewModel = new RoleClaimsViewModel() { Id = roleId };

            foreach (var theClaim in Claims.GetClaims())
            {
                viewModel.Claims.Add(new ClaimViewModel()
                {
                    ClaimType = theClaim.Type,
                    Exist = roleClaims.Exists(x => x.Type == theClaim.Type)
                });
            }

            return View(viewModel);
        }
    }
}
