namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommonInfo")]
    public partial class CommonInfo
    {
        public int CommonInfoID { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Skype { get; set; }

        [StringLength(100)]
        public string Zalo { get; set; }

        [StringLength(200)]
        public string FaceBook { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }

        [StringLength(500)]
        public string MetaKeywords { get; set; }

        [StringLength(500)]
        public string FacebookImageUrl { get; set; }

        [StringLength(50)]
        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int? ModifiedUserId { get; set; }
    }
}
