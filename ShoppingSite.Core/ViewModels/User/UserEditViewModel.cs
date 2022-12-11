using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.User
{
    public class UserEditViewModel : UserViewModel
    {
        [Display(Name = "آدرس")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشد")]
        public string? Address { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
