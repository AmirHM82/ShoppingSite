using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Core.Moderators;
using ShoppingSite.Core.ViewModels.Role;

namespace ShoppingSite.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleModerator roleModerator;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            roleModerator = new(ref roleManager, ModelState);
            roleModerator.RecievedResult += RoleModerator_RecievedResult;
        }

        private void RoleModerator_RecievedResult(List<Core.Models.Operation.Result> results, ref IActionResult returnView)
        {
            if (results.Any())
                foreach (var result in results)
                {
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }

                        returnView = View("Error");
                    }
                }

            //roleModerator.RecievedResult -= RoleModerator_RecievedResult;
        }

        [HttpGet]
        [Authorize(Policy = "CreateRolePolicy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            return await roleModerator.CreateAsync(roleViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "RolesListPolicy")]
        public IActionResult List()
        {
            return View(roleModerator.Roles);
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(string Id)
        {
            return await roleModerator.FindAsync(Id);
        }

        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(RoleViewModel viewModel)
        {
            return await roleModerator.EditAsync(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> Delete(string Id)
        {
            return await roleModerator.DeleteAsync(Id);
        }

        /// <param name="Id">Role Id</param>
        [HttpGet]
        [Authorize(Policy = "EditRoleClaimsPolicy")]
        public async Task<IActionResult> Claims(string Id)
        {
            return await roleModerator.GetClaimsAsync(Id);
        }

        [HttpPost]
        [Authorize(Policy = "EditRoleClaimsPolicy")]
        public async Task<IActionResult> EditClaims(RoleClaimsViewModel viewModel)
        {
            await roleModerator.RemoveAllClaimsAsync(viewModel.Id, null);
            return await roleModerator.AddClaimsAsync(viewModel.Id, RedirectToAction("List"), viewModel.Claims); //Returns a not found page
        }
    }
}
