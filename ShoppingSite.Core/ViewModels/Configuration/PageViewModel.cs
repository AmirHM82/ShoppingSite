using ShoppingSite.Core.ViewModels.Shared;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Configuration
{
    public class PageViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<MetaTagViewModel> Tags { get; set; }
    }
}
