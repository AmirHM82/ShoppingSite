using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.ViewModels.Category;

namespace ShoppingSite.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        public CategoryController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<IActionResult> List()
        {
            return await CategoryModerator.List();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return await CategoryModerator.Add();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel viewModel) //SubCategories object is null, fuuuuuuuuuuuuuuuuuuuck
        {
            return await CategoryModerator.Add(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return await CategoryModerator.Find(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel viewModel)
        {
            return await CategoryModerator.Edit(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return await CategoryModerator.Delete(id);
        }
    }
}
