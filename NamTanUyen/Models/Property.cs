using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("Property")]

    public class Property
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyNameDisplay { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? ModifiedUserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMainProp { get; set; }
        public int? OrderIndex { get; set; }
        public virtual HashSet<PropertyDetail> PropertyDetail { get; set; } = new HashSet<PropertyDetail>();
    }
}