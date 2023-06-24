using Microsoft.AspNetCore.Mvc;

namespace ShoppingSite.Controllers
{
    public class UserProductController : BaseController<UserProductController>
    {
        public UserProductController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
