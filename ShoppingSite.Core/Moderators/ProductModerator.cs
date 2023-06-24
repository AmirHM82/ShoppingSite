using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.Product;
using ShoppingSite.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Moderators
{
    public class ProductModerator : Controller
    {
        public IProduct productService;
        public ModelStateDictionary ModelState { get; }

        public ProductModerator(IProduct productService, ModelStateDictionary modelState)
        {
            this.productService = productService;
            ModelState = modelState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of products</returns>
        public async Task<IActionResult> GetProducts(int page)
        {
            if (page < 1)
                page = 1;

            ViewBag.Page = page;
            ViewBag.TotalPages = await productService.CountPages();

            var products = await productService.GetAsync(page);

            //The Adapt is from Mapster and it does object to object conversion (I converted model to view model here)
            return View(products.Adapt<IEnumerable<ProductViewModel>>());
        }

        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await productService.FindAsync(productId);
            return View(product.Adapt<ProductViewModel>());
        }

        //public async Task<IActionResult> Add()
        //{
        //    var categories = await //Wait, this is not good
        //}

        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = viewModel.Adapt<Product>();

            if (viewModel.Picture is not null)
            {
                string extension = Path.GetExtension(viewModel.Picture.FileName).ToLower();
                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg") return View();
                string fileName = $"{Guid.NewGuid()}{extension}";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\products\\", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await viewModel.Picture.CopyToAsync(stream);

                viewModel.PictureName = fileName;
                model.PictureName = fileName;
                viewModel.PictureFullAddress = filePath;
                model.PictureFullAddress = filePath;
            }

            await productService.AddAsync(model);
            await productService.SaveAsync();

            viewModel.Id = model.Id;
            return View("Index", viewModel);
        }

        public async Task<IActionResult> Edit(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = await productService.FindAsync((int)viewModel.Id);

            if (model is null)
                return View("Error");

            model.Title = viewModel.Title;
            model.Price = viewModel.Price;
            model.Description = viewModel.Description;
            model.Discount = viewModel.Discount;

            if (viewModel.Picture is not null)
            {
                string extension = Path.GetExtension(viewModel.Picture.FileName).ToLower();
                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg") return View();

                if (System.IO.File.Exists(model.PictureFullAddress)) System.IO.File.Delete(model.PictureFullAddress);

                using var stream = new FileStream(model.PictureFullAddress, FileMode.Create);
                await viewModel.Picture.CopyToAsync(stream);

                viewModel.PictureName = model.PictureName;
                viewModel.PictureFullAddress = model.PictureFullAddress;
            }

            productService.Update(model);
            await productService.SaveAsync();


            return View("Index", viewModel);
        }

        public async Task<IActionResult> Delete(int Id, int page)
        {
            if (page < 1)
                page = 1;

            var product = await productService.Remove(Id);

            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.PictureName))
                    if (System.IO.File.Exists(product.PictureFullAddress)) System.IO.File.Delete(product.PictureFullAddress);

                await productService.SaveAsync();
            }

            return RedirectToAction("List", new { page = page });
        }

        public async Task<IActionResult> Search(string text)
        {
            var result = await productService.SearchAsync(text);
            return View(result.Adapt<IEnumerable<ProductViewModel>>());
        }
    }
}
