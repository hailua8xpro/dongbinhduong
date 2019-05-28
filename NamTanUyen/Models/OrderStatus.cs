namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [StringLength(50)]
        public string OrderStatusName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
