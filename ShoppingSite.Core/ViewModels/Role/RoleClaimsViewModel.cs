using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Role
{
    public class RoleClaimsViewModel
    {
        /// <summary>
        /// Role id
        /// </summary>
        [Display(Name = "آیدی نقش")]
        public string Id { get; set; }

        [Display(Name = "دسترسی ها")]
        public List<ClaimViewModel> Claims { get; set; } = new();
    }
}
