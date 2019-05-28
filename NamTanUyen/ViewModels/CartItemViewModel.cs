using NamTanUyen.Models;
using System.Collections.Generic;

namespace NamTanUyen.ViewModels
{
    public class CartItemViewModel
    {
        public int Quantity { get; set; }
        public Product ProductItem { get; set; }
        public string ImageUrl { get; set; }
        public Color Color { get; set; }
    }
}