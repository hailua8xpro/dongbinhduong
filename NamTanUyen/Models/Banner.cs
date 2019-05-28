namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {

        public int BannerId { get; set; }

        [StringLength(500), DisplayName("Tiêu đề")]
        public string Caption { get; set; }

        [StringLength(500)]
        public string Url { get; set; }

        [StringLength(500), DisplayName("Hình đại diện (2048 x 1372)")]
        public string ImageUrl { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int? ModifiedUserId { get; set; }
        [DisplayName("Hiển thị trên đầu trang")]
        public bool IsHeader { get; set; }
        public int? OrderIndex { get; set; }
    }
}
