using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Packaging;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.Category;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public class CategoryModerator : Controller
    {
        private readonly ICategory categoryService;
        private readonly ModelStateDictionary modelState;

        public CategoryModerator(ICategory categoryService, ModelStateDictionary modelState)
        {
            this.categoryService = categoryService;
            this.modelState = modelState;
        }

        public async Task<IActionResult> List()
        {
            var categories = await categoryService.GetAll();
            return View(categories.Adapt<List<CategoryViewModel>>());
        }

        /// <summary>
        /// This is for http GET requests
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            var c = await categoryService.GetAll();
            c = c.DistinctBy(x => x.Name).ToList();
            var viewModel = new CategoryViewModel();
            //{
            //    Categories = c.Adapt<ICollection<CategoryViewModel>>()
            //};

            viewModel.Categories = c.Select(category => category.Name).ToList();

            return View(viewModel);
        }

        /// <summary>
        /// This is for http POST requests
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Add(CategoryViewModel viewModel)
        {
            var selectedCategories = await categoryService.FindAsync(viewModel.Categories);
            var model = viewModel.Adapt<Category>();
            model.SubCategories = selectedCategories;
            await categoryService.AddAsync(model);
            await categoryService.SaveAsync();
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Find(int id)
        {
            var category = await categoryService.FindAsync(id);

            var viewModel = category.Adapt<CategoryViewModel>();
            if (viewModel.Categories is null)
                viewModel.Categories = new List<string>();

            if (category.SubCategories is not null)
                viewModel.Categories.AddRange(category.SubCategories.Select(category => category.Name));

            var allCategories = await categoryService.GetAll();
            if (allCategories is not null)
                viewModel.Categories.AddRange(allCategories.Select(category => category.Name));

            viewModel.Categories = viewModel.Categories.DistinctBy(x => x).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(CategoryViewModel viewModel)
        {
            var categories = await categoryService.FindAsync(viewModel.Categories);
            var model = viewModel.Adapt<Category>();
            model.SubCategories = categories;
            categoryService.UpdateAsync(model);
            await categoryService.SaveAsync();
            //return RedirectToAction("Index", new { id = viewModel.Id });
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int id)
        {
            categoryService.Delete(id);
            await categoryService.SaveAsync();
            return RedirectToAction("List");
        }
    }
}
