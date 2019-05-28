using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NamTanUyen.Models
{
    [Table("Contact")]

    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Comment { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}