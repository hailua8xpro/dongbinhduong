namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int NewsId { get; set; }

        [StringLength(500), DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [DisplayName("Nội dung")]
        public string Content { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }
        [DisplayName("Loại tin tức")]
        public int NewsCategoryId { get; set; }

        [StringLength(500), DisplayName("Tóm tắt")]
        public string Summary { get; set; }
        [DisplayName("Số lần hiển thị")]
        public int? ViewCount { get; set; } = 0;

        [StringLength(500), DisplayName("Ảnh đại diện")]
        public string ImageUrl { get; set; }

        [StringLength(500)]
        public string NewsUrl { get; set; }

        [StringLength(500)]
        public string MetaKeywords { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }
        [DisplayName("Nổi bật")]
        public bool IsHot { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int? ModifiedUserId { get; set; }
        [DisplayName("Thứ tự hiển thị")]

        public int? OrderIndex { get; set; }

        [DisplayName("Loại tin tức")]
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
