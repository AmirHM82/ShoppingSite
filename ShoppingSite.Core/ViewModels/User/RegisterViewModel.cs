using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSite.Core.ViewModels.User
{
    public class RegisterViewModel
    {
        //Attention: Normally we have to use email as an username but here since we should use phone
        [Phone]
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Remote(action: "IsPhoneInUse", controller: "Account")]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور با تکرار آن تناقض دارد")]
        public string ConfirmPassword { get; set; }
    }
}
