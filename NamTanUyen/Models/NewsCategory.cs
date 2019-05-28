namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsCategory")]
    public partial class NewsCategory
    {
        public NewsCategory()
        {
            News = new HashSet<News>();
        }
        public int NewsCategoryId { get; set; }

        [Required]
        [StringLength(500), DisplayName("Loại tin tức")]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string CategoryUrl { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }

        [StringLength(500)]
        public string Metakeywords { get; set; }
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }

        [StringLength(500), DisplayName("Ảnh đại diện")]
        public string ImageUrl { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int? ModifiedUserId { get; set; }
        public virtual HashSet<News> News { get; set; }
        [DisplayName("Thứ tự hiển thị")]
        public int? OrderIndex { get; set; }
    }
}
