using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Role
{
    public class RoleViewModel
    {
        [Display(Name = "آیدی")]
        public string Id { get; set; }

        [Display(Name = "اسم")]
        [Required(ErrorMessage = "نام نقش نباید خالی باشد")]
        public string Name { get; set; }

        public bool Included { get; set; }
    }
}
