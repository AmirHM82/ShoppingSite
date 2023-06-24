using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.User
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "من را به یاد داشته باش")]
        public bool RememberMe { get; set; }
    }
}
