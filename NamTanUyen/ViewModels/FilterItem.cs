using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class FilterItem
    {
        public int OrderNo { get; set; }
        public int PropertyDetailId { get; set; }
        public int TotalProduct { get; set; }
        public string PropertyValue { get; set; }
        public int PropertyId { get; set; }
        public string PropertyNameDisplay { get; set; }
    }
}