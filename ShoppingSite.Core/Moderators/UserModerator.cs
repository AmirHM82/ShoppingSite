using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.User;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public class UserModerator : Controller
    {
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public ModelStateDictionary ModelState { get; }

        public UserModerator(ref UserManager<User> userManager, ref SignInManager<User> signInManager, ModelStateDictionary modelState)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            ModelState = modelState;
        }

        /// <summary>
        /// This is for posts requests
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> SignupAsync(RegisterViewModel viewModel, string? returnUrl)
        {
            //await new UserModerator(userService).Signup(viewModel);

            //Well u have to move all the codes below to user moderator

            if (ModelState.IsValid)
            {
                var user = new User { UserName = viewModel.Phone, PhoneNumber = viewModel.Phone };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, true);

                    //Use local redirect to prevent redirect attacks (They can send user to their site by this attack)
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return LocalRedirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                //The errors will be shown in asp-validation-summery in the View
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }

        /// <summary>
        /// This is for post requests
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> SigninAsync(LoginViewModel viewModel, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            //Method 1:
            //Idk what happens here but this doesn't work for now we use method 2
            //var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password.MD5Encoding(), viewModel.RememberMe, false);

            //if (result.Succeeded)
            //{
            //      if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            //          return LocalRedirect(returnUrl);
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //    //The errors will be shown in asp-validation-summery in the View
            //    ModelState.AddModelError(string.Empty, "ورود شما موفقیت آمیز نبود");

            //Method 2:
            try
            {
                var user = await UserManager.FindByNameAsync(viewModel.UserName);
                var PasswordMatch = await UserManager.CheckPasswordAsync(user, viewModel.Password);
                await SignInManager.SignInAsync(user, viewModel.RememberMe);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
                return View(viewModel);
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignoutAsync()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SearchAsync(string query)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await UserManager.Users.Where(x => x.UserName.Contains(query) || x.Email.Contains(query) || x.PhoneNumber.Contains(query)).ToListAsync();

            return View(new UserSearchResultViewModel()
            {
                Query = query,
                Users = result.Adapt<List<UserViewModel>>()
            });
        }

        /// <summary>
        /// This is for get requests
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>An user viewModel</returns>
        public async Task<IActionResult> EditAsync(string userId)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await UserManager.FindByIdAsync(userId);

            return View(user.Adapt<UserEditViewModel>());
        }

        /// <summary>
        /// This is for post requests
        /// </summary>
        /// <param name="viewModel">The user whom pased from view</param>
        public async Task<IActionResult> EditAsync(UserEditViewModel viewModel, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await UserManager.FindByIdAsync(viewModel.Id);
            user.UserName = viewModel.UserName;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.Email = viewModel.Email;
            user.Address = viewModel.Address;
            var result = await UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(viewModel);
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await UserManager.FindByNameAsync(email);

            if (user == null)
                return Json(true);
            else
                return Json($"{email} در سایت ثبت شده است لطفا ایمیل دیگری وارد کنید");
        }

        public async Task<IActionResult> IsPhoneInUse(string phone)
        {
            var user = await UserManager.FindByNameAsync(phone);

            if (user == null)
                return Json(true);
            else
                return Json($"{phone} در سایت ثبت شده است لطفا شماره دیگری وارد کنید");
        }
    }
}
