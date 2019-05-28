using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class PagerViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public string Url { get; set; }
    }
}