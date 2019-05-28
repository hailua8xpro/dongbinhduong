namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            ProductImage = new HashSet<Models.ProductImage>();
        }
        public int ProductId { get; set; }

        [Required]
        [StringLength(500), DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string ProductNameUrl { get; set; }

        [DataType(DataType.Currency), DisplayName("Giá")]
        public decimal? Price { get; set; } = 0;

        [StringLength(500), DisplayName("Hình đại diện (265 x 265)")]
        public string ImageUrl { get; set; }

        [StringLength(500), DisplayName("Mô tả sản phẩm")]
        public string ProductDesciption { get; set; }
        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [StringLength(500)]
        public string Metakeywords { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        [DisplayName("Diện tích")]

        public int? Area { get; set; }
        [DisplayName("Thổ cư")]
        public int? Area2 { get; set; }
        [DisplayName("Tỷ lệ")]
        public string Ratio { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DisplayName("Giá khuyến mại")]
        public decimal? SalePrice { get; set; } = 0;
        [DisplayName("Khuyến mại")]
        public bool IsHasPromotion { get; set; }
        [DisplayName("Mới")]
        public bool IsNew { get; set; }
        [DisplayName("Nổi bật")]
        public bool IsBestSell { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int? ProductCategoryId { get; set; }
        [DisplayName("Nhóm sản phẩm")]
        public int? ProductGroupId { get; set; }
        [DisplayName("Đối tượng sản phẩm")]
        public int ProductObjectId { get; set; }

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int CreatedUserId { get; set; }
        [DisplayName("Tóm tắt")]
        public string Summary { get; set; }
        public int? ModifiedUserId { get; set; }
        [DisplayName("Loại sản phẩm chính")]
        public virtual ProductCategory ProductCategory { get; set; }
        [DisplayName("Nhóm sản phẩm chính")]
        public virtual ProductGroup ProductGroup { get; set; }
        [DisplayName("Đối tượng sản phẩm")]
        public virtual ProductObject ProductObject { get; set; }
        [DisplayName("Bộ sưu tập")]
        public virtual HashSet<ProductImage> ProductImage { get; set; }
        [DisplayName("Thương hiệu")]
        public int? BrandId { get; set; }
        [DisplayName("Thương hiệu")]
        public virtual Brand Brand { get; set; }
        [DisplayName("Thứ tự hiển thị")]
        public int? OrderIndex { get; set; }
        public string ListPropertyValue { get; set; }
        public string VideoUrl { get; set; }
        [DataType(DataType.MultilineText)]
        public string Map { get; set; }
        public string Phone { get; set; }
        public virtual HashSet<ProductProperty> ProductProperty { get; set; } = new HashSet<ProductProperty>();

    }
}
