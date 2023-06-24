using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.ViewModels.User;
using ShoppingSite.Core.Moderators;
using ShoppingSite.DAL.Entities;

namespace ShoppingSite.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        //private readonly UserModerator UserModerator;
        //private readonly UserRolesModerator userRolesModerator;

        //public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        //{
        //    UserModerator = new(userManager, signInManager, ModelState);
        //    userRolesModerator = new(ref userManager, ref roleManager, ModelState);
        //}

        public AccountController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        [AcceptVerbs("Get", "Post")] //= [HttpPost][HttpGet]
        public async Task<IActionResult> IsPhoneInUse(string phone)
        {
            return await UserModerator.IsPhoneInUse(phone);
        }

        [AcceptVerbs("Get", "Post")] //= [HttpPost][HttpGet]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            return await UserModerator.IsEmailInUse(email);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterViewModel viewModel, string? returnUrl)
        {
            return await UserModerator.SignupAsync(viewModel, returnUrl);
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(LoginViewModel viewModel/*, string? returnUrl*/)
        {
            return await UserModerator.SigninAsync(viewModel/*, returnUrl*/);
        }

        //[HttpPost]
        public async Task<IActionResult> Signout()
        {
            return await UserModerator.SignoutAsync();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "SearchAccountPolicy")]
        public async Task<IActionResult> Search(string query)
        {
            return await UserModerator.SearchAsync(query);
        }

        [HttpGet]
        [Authorize(Policy = "EditAccountPolicy")]
        public async Task<IActionResult> Edit(string userId)
        {
            return await UserModerator.EditAsync(userId ?? UserId);
        }

        [HttpPost]
        [Authorize(Policy = "EditAccountPolicy")]
        public async Task<IActionResult> Edit(UserEditViewModel viewModel, string? returnUrl)
        {
            return await UserModerator.EditAsync(viewModel, returnUrl);
        }

        //The part bellow is untested

        [HttpGet]
        [Authorize(Policy = "EditAccountRolesPolicy")]
        public async Task<IActionResult> EditRoles(string userId)
        {
            return await UserRolesModerator.FindRoles(userId);
        }

        [HttpPost]
        [Authorize(Policy = "EditAccountRolesPolicy")]
        public async Task<IActionResult> EditRoles(UserRolesViewModel viewModel, string? returnUrl)
        {
            return await UserRolesModerator.EditRoles(viewModel);
        }
    }
}
