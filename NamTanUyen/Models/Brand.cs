using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NamTanUyen.Models
{
    [Table("Brand")]

    public class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
        }
        [Key]
        public int BrandId { get; set; }

        [Required]
        [StringLength(500), DisplayName("Tên thương hiệu")]
        public string BrandName { get; set; }

        [StringLength(500)]
        public string BrandUrl { get; set; }

        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }

        [StringLength(500)]
        public string Metakeywords { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }

        [StringLength(500), DisplayName("Hình đại diện (265 x 265)")]
        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }
        public virtual HashSet<Product> Product { get; set; }
        [DisplayName("Hiển thị footer")]
        public bool IsFooter { get; set; }
        [DisplayName("Nổi bật")]
        public bool IsHot { get; set; }
        [DisplayName("Thứ tự hiển thị")]
        public int? OrderIndex { get; set; }
    }
}