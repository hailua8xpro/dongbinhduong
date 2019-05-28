namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StaticPage")]
    public partial class StaticPage
    {
        public int StaticPageId { get; set; }

        [StringLength(500), DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [StringLength(500)]
        public string MetaKeyword { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }
        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [StringLength(500), DisplayName("Tên trang")]
        public string PageName { get; set; }

        [StringLength(500)]
        public string PageUrl { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }
        [DisplayName("Tóm tắt")]
        public string Summary { get; set; }
        [DisplayName("Ảnh đại diện")]
        public string ImageUrl { get; set; }
        [DisplayName("Hiển thị trang chủ")]
        public bool InHome { get; set; }
    }
}
