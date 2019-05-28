using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("Collection")]

    public class Collection
    {
        public int CollectionId { get; set; }
        [Required]
        [StringLength(500)]

        public string CollectionName { get; set; }
        [StringLength(500)]
        public string CollectionUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public int OrderIndex { get; set; }
        [StringLength(500)]
        public string Metakeywords { get; set; }

        [StringLength(500)]
        public string MetaDescription { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int CreatedUserId { get; set; }

        public int? ModifiedUserId { get; set; }
    }
}