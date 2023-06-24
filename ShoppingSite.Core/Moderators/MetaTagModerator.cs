using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.Configuration;
using ShoppingSite.DAL.Entities;

namespace ShoppingSite.Core.Moderators
{
    public class MetaTagModerator : Controller
    {
        public IViewPage viewPageService;
        public IMetaTag metaTagService;
        public ModelStateDictionary ModelState { get; }

        public MetaTagModerator(IViewPage viewPageService, IMetaTag metaTagService, ModelStateDictionary modelState)
        {
            this.viewPageService = viewPageService;
            this.metaTagService = metaTagService;
            ModelState = modelState;
        }

        public async Task<IActionResult> Index(string url)
        {
            var page = await viewPageService.FindAsync(url);
            ViewBag.Url = url;

            return View(page?.Tags?.Adapt<IEnumerable<MetaTagViewModel>>());
        }

        public async Task<IActionResult> RemoveTag(string url, int tagId)
        {
            //Remove tag from PageView table
            var page = await viewPageService.FindAsync(url);
            var tag = await metaTagService.FindAsync(tagId);
            page.Tags.Remove(tag);
            //Then remove ot from MetaTag table
            metaTagService.Remove(tag);
            await viewPageService.SaveAsync();

            if (!page.Tags.Any())
            {
                viewPageService.Remove(page);
                await viewPageService.SaveAsync();
            }

            return RedirectToAction("Index", new { url = url });
        }

        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddTag(string url)
        {
            ViewBag.Url = url;
            return View();
        }

        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="metaTag"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddTag(MetaTagViewModel metaTag, string url)
        {
            //Add it to MetaTag table
            var tag = metaTag.Adapt<MetaTag>();
            tag = (await metaTagService.AddAsync(tag)).Entity;
            await metaTagService.SaveAsync();

            //then Add tag to PageView table
            var page = await viewPageService.FindAsync(url);
            if (page is not null)
            {
                viewPageService.Update(page);
                page.Tags.Add(tag);
            }
            else
            {
                page = new();
                page.Url = url;
                if (page.Tags is null)
                    page.Tags = new List<MetaTag>();
                page.Tags.Add(tag);

                await viewPageService.AddAsync(page);
            }
            await viewPageService.SaveAsync();

            return RedirectToAction("Index", new { url = url });
        }

        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditTag(int tagId, string url)
        {
            ViewBag.Url = url;
            var tag = await metaTagService.FindAsync(tagId);
            return View(tag.Adapt<MetaTagViewModel>());
        }

        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="metaTag"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditTag(MetaTagViewModel metaTag, string url)
        {
            var pTag = await metaTagService.FindAsync(metaTag.Id);

            if (!pTag.Equals(metaTag))
            {
                pTag.Name = metaTag.Name;
                pTag.Content = metaTag.Content;
            }

            metaTagService.Update(pTag);
            await metaTagService.SaveAsync();

            return RedirectToAction("Index", new { url = url });
        }
    }
}
