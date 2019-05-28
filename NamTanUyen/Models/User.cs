namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string PassWord { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public int? Gender { get; set; }
    }
}
