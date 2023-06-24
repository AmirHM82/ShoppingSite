using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingSite.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public class SubCategoryModerator : Controller
    {
        private readonly ISubCategory subCategoryService;
        private readonly ModelStateDictionary modelState;

        public SubCategoryModerator(ISubCategory subCategoryService, ModelStateDictionary modelState)
        {
            this.subCategoryService = subCategoryService;
            this.modelState = modelState;
        }
    }
}
