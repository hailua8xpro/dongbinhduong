using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        [DisplayName("Tên khách hàng")]
        public string FullName { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        public string Email { get; set; }
        [DisplayName("Tổng SL")]
        public int TotalQuantity { get; set; }
        [DisplayName("Tổng tiền")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Mã ĐH")]
        public string OrderCode { get; set; }
        [DisplayName("Ngày đặt hàng")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Trạng thái")]
        public string OrderStatusName { get; set; }
        public decimal PageCount { get; set; }
    }
}