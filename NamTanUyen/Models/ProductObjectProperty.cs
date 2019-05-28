using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("ProductObject_Property")]
    public class ProductObjectProperty
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ProductObjectId { get; set; }
        public virtual ProductObject ProductObject { get; set; }
        public virtual Property Property { get; set; }
    }
}