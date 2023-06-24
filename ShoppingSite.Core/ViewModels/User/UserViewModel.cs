using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "آیدی")]
        public string Id { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشد")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "موبایل")]
        [Phone]
        [Required]
        public string? PhoneNumber { get; set; }
    }
}
