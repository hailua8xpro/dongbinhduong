using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class ProductItem
    {
        //[DisplayName("Đối tượng sản phẩm")]
        //public string ProductObjectName { get; set; }
        //[DisplayName("Loại sản phẩm")]
        //public string ProductCategoryName { get; set; }
        //[DisplayName("Nhóm sản phẩm")]
        //public string ProductGroupName { get; set; }
        public int? OrderIndex { get; set; }
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int? ProductGroupId { get; set; }
        public int ProductObjectId { get; set; }
        public int? BrandId { get; set; }
        public int? Area { get; set; }
        [DisplayName("Thổ cư")]
        public int? Area2 { get; set; }
        [DisplayName("Tỷ lệ")]
        public string Ratio { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        public string ProductGroupUrl { get; set; }
        public string ProductCategoryNameUrl { get; set; }
        public string ProductObjectUrl { get; set; }
        [DisplayName("Giá")]
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string ImageUrl { get; set; }
        [DisplayName("Kích hoạt")]
        public bool IsActive { get; set; }

        public bool IsHasPromotion { get; set; }
        public bool IsNew { get; set; }
        public bool IsBestSell { get; set; }
        [DisplayName("Màu")]
        public decimal TotalPage { get; set; }
        public string ProductNameUrl { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ListPropertyValue { get; set; }
    }
}