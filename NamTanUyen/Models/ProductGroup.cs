namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductGroup")]
    public partial class ProductGroup
    {
        [DisplayName("Nhóm sản phẩm")]

        public int ProductGroupId { get; set; }

        public int? ProductCategoryId { get; set; }
        public int ProductObjectId { get; set; }

        [Required]
        [StringLength(500)]
        public string ProductGroupName { get; set; }

        [StringLength(500)]
        public string ProductGroupUrl { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string Metakeywords { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public int CreatedUserId { get; set; }
        public int? ModifiedUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsBuild { get; set; }
        public int? BuildOrderIndex { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductObject ProductObject { get; set; }
        public virtual HashSet<Product> Product { get; set; } = new HashSet<Models.Product>();

    }
}
