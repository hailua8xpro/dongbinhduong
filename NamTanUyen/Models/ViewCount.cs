using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("ViewCount")]
    public class ViewCount
    {
        [Key]
        public int ViewCountId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Note { get; set; }
    }
}