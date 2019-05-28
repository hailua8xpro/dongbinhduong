using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("Product_Property")]
    public class ProductProperty
    {
        public int Id { get; set; }
        public int PropertyDetailId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual PropertyDetail PropertyDetail { get; set; }
    }
}