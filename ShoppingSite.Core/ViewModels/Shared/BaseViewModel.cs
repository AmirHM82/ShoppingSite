using ShoppingSite.Core.ViewModels.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Shared
{
    public class BaseViewModel
    {
        public Stack<string>? ReturnUrls { get; set; }
    }
}
