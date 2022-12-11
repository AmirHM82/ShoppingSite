using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "کد محصول")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [MaxLength(20, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Name { get; set; }
    }
}
