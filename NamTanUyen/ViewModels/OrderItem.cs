using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class OrderItem
    {
        public int OrderID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string OrderNote { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ShipAddress { get; set; }
        public string Address { get; set; }

        public decimal? TotalAmount { get; set; }
        public int? TotalQuantity { get; set; }

        public string FullName { get; set; }
        public string OrderCode { get; set; }
    }
}