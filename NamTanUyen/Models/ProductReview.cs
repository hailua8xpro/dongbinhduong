namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductReview")]
    public partial class ProductReview
    {
        public int ProductReviewId { get; set; }

        public int? UserId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(500)]
        public string ProductReviewTitle { get; set; }

        [Required]
        public string ProductReviewContent { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public int Rating { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserId { get; set; }
    }
}
