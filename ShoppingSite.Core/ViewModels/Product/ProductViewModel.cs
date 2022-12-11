using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels.Product
{
    public class ProductViewModel
    {
        [Display(Name = "کد محصول")]
        public int? Id { get; set; }

        [Display(Name = "عنوان محصول")]
        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشد")]
        [Required(ErrorMessage = "وارد کردن عنوان ضروری است")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "تعیین قیمت ضروری است")]
        public int Price { get; set; }

        [Display(Name = "تخفیف")]
        public int? Discount { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "اسم تصویر")]
        public string PictureName { get; set; }

        [Display(Name = "آدرس تصویر")]
        public string PictureFullAddress { get; set; }

        [Display(Name = "دسته بندی ها")]
        public List<CategoryViewModel>? Categories { get; set; } = new();
    }
}
