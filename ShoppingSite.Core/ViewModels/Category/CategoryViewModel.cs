using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingSite.Core.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Category
{
    public class CategoryViewModel
    {
        [Display(Name = "کد محصول")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Name { get; set; }
        public List<string>? Categories { get; set; }
        //public ICollection<ProductViewModel> Products { get; set; }
        //public ICollection<CategoryViewModel> SubCategories { get; set; }

        //public List<SelectListItem> SubCategories { get; set; }
    }
}
