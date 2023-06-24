using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.Moderators;
using ShoppingSite.DAL.Entities;
using System.Security.Claims;

namespace ShoppingSite.Controllers
{
    public abstract class BaseController<T> : BaseModerator<T> where T : BaseController<T>
    {
        protected BaseController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
