using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.DAL.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<Category> Categories { get; set; }
        public bool IsHidden { get; set; }
        public bool IsInRecyclebin { get; set; }
        public User Adder { get; set; }
        public string PictureName { get; set; }
        public string PictureFullAddress { get; set; }
    }
}
