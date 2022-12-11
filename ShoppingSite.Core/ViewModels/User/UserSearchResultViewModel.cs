using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.User
{
    public class UserSearchResultViewModel
    {
        public UserSearchResultViewModel()
        {
            Users = new();
        }

        public string Query { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}
