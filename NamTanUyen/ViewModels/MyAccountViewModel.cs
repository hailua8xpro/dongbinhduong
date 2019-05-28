using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class MyAccountViewModel
    {
        public int UserId { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
    }
}