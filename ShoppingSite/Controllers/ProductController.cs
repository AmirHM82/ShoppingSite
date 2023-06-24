using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingSite.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        public ProductController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [HttpGet]
        [Authorize(Policy = "AddProductPolicy")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "AddProductPolicy")]
        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            return await ProductModerator.Add(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "EditProductPolicy")]
        public async Task<IActionResult> Edit(int id)
        {
            return await ProductModerator.GetProduct(id);
        }

        [HttpPost]
        [Authorize(Policy = "EditProductPolicy")]
        public async Task<IActionResult> Edit(ProductViewModel viewModel)
        {
            return await ProductModerator.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "IndexProductPolicy")]
        public IActionResult Index(ProductViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet]
        //[Authorize(Policy = "IndexProductPolicy")]
        public async Task<IActionResult> Index(int id)
        {
            return await ProductModerator.GetProduct(id);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteProductPolicy")]
        public async Task<IActionResult> Delete(int id, int page)
        {
            return await ProductModerator.Delete(id, page);
        }

        //[Authorize(Policy = "ProductListPolicy")]
        public async Task<IActionResult> List(int page)
        {
            return await ProductModerator.GetProducts(page);
        }

        //[Authorize(Policy = "ManageProductsPolicy")]
        //public async Task<IActionResult> Manage(int page)
        //{
        //    return await ProductModerator.GetProducts(page);
        //}

        [HttpGet]
        //[Authorize(Policy = "SearchProductPolicy")]
        public async Task<IActionResult> Search(string text)
        {
            return await ProductModerator.Search(text);
        }
    }
}
