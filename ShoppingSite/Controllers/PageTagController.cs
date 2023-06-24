using Mapster;
using Microsoft.AspNetCore.Mvc;
using ShoppingSite.Core.ViewModels.Configuration;
using ShoppingSite.DAL.Entities;

namespace ShoppingSite.Controllers
{
    public class PageTagController : BaseController<PageTagController>
    {
        public PageTagController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<IActionResult> Index(string url)
        {
            return await MetaTagModerator.Index(url);
        }

        public async Task<IActionResult> RemoveTag(string url, int tagId)
        {
            return await MetaTagModerator.RemoveTag(url, tagId);
        }

        public async Task<IActionResult> AddTag(string url)
        {
            return await MetaTagModerator.AddTag(url);
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(MetaTagViewModel metaTag, string url)
        {
            return await MetaTagModerator.AddTag(metaTag, url);
        }

        public async Task<IActionResult> EditTag(int tagId, string url)
        {
            return await MetaTagModerator.EditTag(tagId, url);
        }

        [HttpPost]
        public async Task<IActionResult> EditTag(MetaTagViewModel metaTag, string url)
        {
            return await MetaTagModerator.EditTag(metaTag, url);
        }
    }
}
