namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        public int ProductImageId { get; set; }

        [StringLength(500), DisplayName("Hình đại diện (640 x 408)")]
        public string ImageUrl { get; set; }
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }
        [DisplayName("Sản phẩm")]
        public int ProductId { get; set; }
        [DisplayName("Sản phẩm")]
        public virtual Product Product { get; set; }
    }
}
