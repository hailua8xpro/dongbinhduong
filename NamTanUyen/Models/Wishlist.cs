namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wishlist")]
    public partial class Wishlist
    {
        public int WishlistId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual Product Product { get; set; }
    }
}
