using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Role
{
    public class ClaimViewModel
    {
        [Display(Name = "دسترسی")]
        public string ClaimType { get; set; }

        [Display(Name = "وضعیت دسترسی")]
        public bool Exist { get; set; }
    }
}
