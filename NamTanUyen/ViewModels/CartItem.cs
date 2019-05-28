using System.Collections.Generic;
using NamTanUyen.Models;

namespace NamTanUyen.ViewModels
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}