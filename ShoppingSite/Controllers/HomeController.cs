using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.Moderators;
using System.Diagnostics;

namespace ShoppingSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected IProduct productService;
        protected ProductModerator moderator;

        public HomeController(ILogger<HomeController> logger, IProduct productService)
        {
            _logger = logger;
            this.productService = productService;
            moderator = new ProductModerator(productService, ModelState);
        }

        public async Task<IActionResult> Index()
        {
            return await moderator.GetProducts(1);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}