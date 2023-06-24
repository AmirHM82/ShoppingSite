using Microsoft.AspNetCore.Mvc;

namespace ShoppingSite.Controllers
{
    public class SubCategoryController : BaseController<SubCategoryController>
    {
        public SubCategoryController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
