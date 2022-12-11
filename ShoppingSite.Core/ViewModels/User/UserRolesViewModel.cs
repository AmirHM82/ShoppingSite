using ShoppingSite.Core.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.User
{
    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        {
            Roles = new();
        }

        public string Id { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
