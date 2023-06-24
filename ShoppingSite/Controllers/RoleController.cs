using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Core.Moderators;
using ShoppingSite.Core.ViewModels.Role;

namespace ShoppingSite.Controllers
{
    public class RoleController : BaseController<RoleController>
    {
        public RoleController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            RoleModerator.RecievedResult += RoleModerator_RecievedResult;
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
            return await RoleModerator.CreateAsync(roleViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "RolesListPolicy")]
        public IActionResult List()
        {
            return View(RoleModerator.Roles);
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(string Id)
        {
            return await RoleModerator.FindAsync(Id);
        }

        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> Edit(RoleViewModel viewModel)
        {
            return await RoleModerator.EditAsync(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> Delete(string Id)
        {
            return await RoleModerator.DeleteAsync(Id);
        }

        /// <param name="Id">Role Id</param>
        [HttpGet]
        [Authorize(Policy = "EditRoleClaimsPolicy")]
        public async Task<IActionResult> Claims(string Id)
        {
            return await RoleModerator.GetClaimsAsync(Id);
        }

        [HttpPost]
        [Authorize(Policy = "EditRoleClaimsPolicy")]
        public async Task<IActionResult> EditClaims(RoleClaimsViewModel viewModel)
        {
            await RoleModerator.RemoveAllClaimsAsync(viewModel.Id, null);
            return await RoleModerator.AddClaimsAsync(viewModel.Id, RedirectToAction("List"), viewModel.Claims); //Returns a not found page
        }
    }
}
