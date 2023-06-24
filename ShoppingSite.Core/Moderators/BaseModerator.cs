using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.Moderators;
using ShoppingSite.Core.ViewModels.Configuration;
using ShoppingSite.DAL.Entities;
using System.Security.Claims;

namespace ShoppingSite.Core.Moderators
{
    public abstract class BaseModerator<T> : Controller where T : BaseModerator<T>
    {
        private ILogger<T>? _logger;
        private SignInManager<User>? _signInManager;
        private UserManager<User>? _userManager;
        private ProductModerator? _productModerator;
        private UserModerator? _userModerator;
        private RoleModerator? _roleModerator;
        private RoleManager<IdentityRole>? _roleManager;
        private UserRolesModerator? _userRolesModerator;
        private MetaTagModerator? _metaTagModerator;
        private CategoryModerator? _categoryModerator;
        private SubCategoryModerator? _subCategoryModerator;
        private IProduct? _productService;
        private IMetaTag? _metaTagService;
        private IViewPage? _viewPageService;
        private ICategory? _categoryService;
        private ISubCategory? _subCategoryService;
        private ClaimsPrincipal _currentUser;

        public SignInManager<User> SignInManager
            => _signInManager ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();

        public UserManager<User> UserManager
            => _userManager ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

        public ProductModerator ProductModerator
            => _productModerator ??= new ProductModerator(ProductService, ModelState);

        public UserModerator UserModerator
            => _userModerator ??= new UserModerator(UserManager, SignInManager, ModelState);

        public RoleManager<IdentityRole> RoleManager
            => _roleManager ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();

        public RoleModerator RoleModerator
            => _roleModerator ??= new RoleModerator(RoleManager, ModelState);

        public UserRolesModerator UserRolesModerator
            => _userRolesModerator ??= new UserRolesModerator(UserManager, RoleManager, ModelState);

        public MetaTagModerator MetaTagModerator
           => _metaTagModerator ??= new MetaTagModerator(ViewPageService, MetaTagService, ModelState);

        public CategoryModerator CategoryModerator
           => _categoryModerator ??= new CategoryModerator(CategoryService, ModelState);

        public SubCategoryModerator SubCategoryModerator
           => _subCategoryModerator ??= new SubCategoryModerator(SubCategoryService, ModelState);

        public IProduct ProductService
            => _productService ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<IProduct>();

        public IMetaTag MetaTagService
            => _metaTagService ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<IMetaTag>();

        public ICategory CategoryService
            => _categoryService ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<ICategory>();

        public ISubCategory SubCategoryService
            => _subCategoryService ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<ISubCategory>();

        public IViewPage ViewPageService
            => _viewPageService ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<IViewPage>();

        public ILogger<T> Logger
            => _logger ??= HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        public ClaimsPrincipal CurrentUser
            => _currentUser ??= HttpContextAccessor.HttpContext.User;

        public IConfiguration Configuration
            => HttpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        public string UserId
        {
            get
            {
                if (User != null)
                {
                    var userIdNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    if (userIdNameClaim != null)
                    {
                        return userIdNameClaim.Value;
                    }
                }

                return null;
            }
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public BaseModerator(IHttpContextAccessor httpContextAccessor) => HttpContextAccessor = httpContextAccessor;

        //public override async void OnActionExecuting(ActionExecutingContext context)
        //{
        //    HttpContextAccessor.HttpContext.Items["MetaTags"] = ((await ViewPageService.FindAsync(context.HttpContext.Request.Path.Value))?.Tags)
        //        .Adapt<ICollection<MetaTagViewModel>>(); //Fuck!
        //    base.OnActionExecuting(context);
        //}
    }
}
