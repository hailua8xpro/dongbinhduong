using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("ProjectImage")]
    public class ProjectImage
    {
        [Key]
        public int ProjectImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int ModifiedUserId { get; set; }
        public bool IsVideo { get; set; }
        public string VideoUrl { get; set; }
    }
}