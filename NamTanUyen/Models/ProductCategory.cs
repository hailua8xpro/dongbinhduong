namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Product = new HashSet<Product>();
            ProductGroup = new HashSet<ProductGroup>();
        }
        [DisplayName("Loại sản phẩm")]

        public int ProductCategoryId { get; set; }
        public int ProductObjectId { get; set; }
        [Required]
        [StringLength(500)]
        public string ProductCategoryName { get; set; }

        [StringLength(500)]
        public string ProductCategoryNameUrl { get; set; }

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

        public int? ModifiedUserId { get; set; }
        public virtual HashSet<Product> Product { get; set; }
        public virtual HashSet<ProductGroup> ProductGroup { get; set; }
        public virtual ProductObject ProductObject { get; set; }
        public bool IsMainCate { get; set; }
    }
}
