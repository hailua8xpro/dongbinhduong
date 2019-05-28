namespace NamTanUyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminUser")]
    public partial class AdminUser
    {
        public int AdminUserId { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int? ModifiedUserId { get; set; }
        public int CreaterUserId { get; set; }
    }
}
