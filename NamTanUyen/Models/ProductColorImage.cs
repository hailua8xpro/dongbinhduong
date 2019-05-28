namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductColorImage")]
    public partial class ProductColorImage
    {
        public int ProductColorImageId { get; set; }

        public int? ProductColorId { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
