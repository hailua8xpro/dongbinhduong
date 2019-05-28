using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("Product_ProductGroup")]
    public class ProductProductGroup
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductGroupId { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
    }
}