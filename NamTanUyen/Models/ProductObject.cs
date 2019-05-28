using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("ProductObject")]
    public class ProductObject
    {
        public ProductObject()
        {
            Product = new HashSet<Product>();
            ProductGroup = new HashSet<ProductGroup>();
            ProductCategory = new HashSet<ProductCategory>();
        }
        [DisplayName("Đối tượng sản phẩm")]
        public int ProductObjectId { get; set; }

        [Required]
        [StringLength(500)]
        public string ProductObjectName { get; set; }

        [StringLength(500)]
        public string ProductObjectUrl { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string Metakeywords { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public int CreatedUserId { get; set; }
        public string BannerImageUrl { get; set; }
        public int? OrderIndex { get; set; }
        public string BannerUrl { get; set; }
        public int? ModifiedUserId { get; set; }
        public virtual HashSet<Product> Product { get; set; }
        public virtual HashSet<ProductGroup> ProductGroup { get; set; }
        public virtual HashSet<ProductCategory> ProductCategory { get; set; }
    }
}