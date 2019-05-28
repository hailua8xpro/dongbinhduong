using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("CollectionDetail")]

    public class CollectionDetail
    {
        public int CollectionDetailId { get; set; }
        public int CollectionId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Collection Collection { get; set; }

    }
}