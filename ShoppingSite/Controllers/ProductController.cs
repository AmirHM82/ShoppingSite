using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.Product;
using ShoppingSite.Core.Moderators;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingSite.Controllers
{
    public class ProductController : Controller
    {
        protected IProduct productService;
        protected ProductModerator moderator;

        public ProductController(IProduct productService)
        {
            this.productService = productService;
            moderator = new ProductModerator(productService, ModelState);
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
            return await moderator.Add(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "EditProductPolicy")]
        public async Task<IActionResult> Edit(int Id)
        {
            return await moderator.GetProduct(Id);
        }

        [HttpPost]
        [Authorize(Policy = "EditProductPolicy")]
        public async Task<IActionResult> Edit(ProductViewModel viewModel)
        {
            return await moderator.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "IndexProductPolicy")]
        public IActionResult Index(ProductViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "IndexProductPolicy")]
        public async Task<IActionResult> Index(int Id)
        {
            return await moderator.GetProduct(Id);
        }

        [HttpGet]
        [Authorize(Policy = "DeleteProductPolicy")]
        public async Task<IActionResult> Delete(int Id, int Page) //Page is 0 (Which is actually null)
        {
            return await moderator.Delete(Id, Page);
        }

        [Authorize(Policy = "ProductListPolicy")]
        public async Task<IActionResult> List(int page)
        {
            return await moderator.GetProducts(page);
        }

        [Authorize(Policy = "ManageProductsPolicy")]
        public async Task<IActionResult> Manage(int page)
        {
            return await moderator.GetProducts(page);
        }
    }
}
