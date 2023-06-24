using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.Moderators;
using System.Diagnostics;

namespace ShoppingSite.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        public HomeController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            
        }

        public async Task<IActionResult> Index()
        {
            return await ProductModerator.GetProducts(1);
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