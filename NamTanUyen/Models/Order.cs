namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string OrderNote { get; set; }

        public int OrderStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(500)]
        public string ShipAddress { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string OrderCode { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }
        public virtual HashSet<OrderDetail> OrderDetail { get; set; } = new HashSet<OrderDetail>();
    }
}
