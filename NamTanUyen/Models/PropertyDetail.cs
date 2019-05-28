using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("PropertyDetail")]
    public class PropertyDetail
    {
        public int PropertyDetailId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? ModifiedUserId { get; set; }
        public bool IsActive { get; set; }
        public virtual Property Property { get; set; }
    }
}