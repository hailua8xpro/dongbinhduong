using NamTanUyen.Models;
using NamTanUyen.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamTanUyen.Controllers
{
    public class OrderController : BaseController
    {
        private JewelryContext db = new JewelryContext();

        // GET: Order
        public ActionResult Index()
        {
            var user = GetCurrentUser();
            var listcartitem = Session["SessionCart"] as List<CartItem>;
            if (listcartitem == null)
            {
                return Redirect("/gio-hang");
            }
            ViewBag.MetaModel = new MetaModel
            {
                Description = "Đặt hàng",
                Keywords = "Đặt hàng",
                Title = "Đặt hàng",
                URL = "/dat-hang"
            };
            return View(user);
        }
        [HttpPost]
        public ActionResult SubmitOrder(Order model)
        {
            if (ModelState.IsValid)
            {
                var commoninfo = db.CommonInfoes.FirstOrDefault();
                string mailfrom = commoninfo == null ? "" : commoninfo.EmailFrom;
                string mailto = commoninfo == null ? "" : commoninfo.EmailTo;
                string[] mails = { };
                if (!string.IsNullOrEmpty(mailto))
                {
                    mails = mailto.Split(',');
                }
                var listcartitem = Session["SessionCart"] as List<CartItem>;
                if (listcartitem != null && listcartitem.Any())
                {
                    model.CreatedDate = DateTime.Now;
                    db.Orders.Add(model);
                    db.SaveChanges();
                    var id = model.OrderId;
                    foreach (var item in listcartitem)
                    {
                        var product = db.Products.Find(item.ProductId);
                        var orderdetail = new OrderDetail
                        {
                            OrderId = id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = product.SalePrice > 0 && product.IsHasPromotion ? product.SalePrice : product.Price
                        };
                        db.OrderDetails.Add(orderdetail);
                    }
                    db.SaveChanges();
                    var orderdetails = db.OrderDetails.Where(x => x.OrderId == id).Include(o => o.Order).Include(o => o.Product);
                    Session["SessionCart"] = null;
                    string subject = "Đơn hàng - " + model.FullName;
                    string body = RenderPartialViewToString("_OrderEmail", orderdetails);
                    Library.Helper.MailHelper.SendMail(mailfrom, subject, body, commoninfo.PassWord, 25, "smtp.gmail.com", mailto: mails);
                    return ReturnJson(result: 1);
                }
                else
                    ReturnJson("Phiên làm việc đã hết hạn, xin vui lòng thử lại");

            }

            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(errors);
        }

        public ActionResult Cart()
        {
            ViewBag.MetaModel = new MetaModel
            {
                Description = "Giỏ hàng",
                Keywords = "Giỏ hàng",
                Title = "Giỏ hàng",
                URL = "/gio-hang"
            };
            var listcartitem = Session["SessionCart"] as List<CartItem>;
            List<CartItemViewModel> lstcartviewmodel = null;
            if (listcartitem != null && listcartitem.Any())
            {
                lstcartviewmodel = new List<CartItemViewModel>();
                foreach (var item in listcartitem)
                {
                    var cartitemviewmodel = new CartItemViewModel { Quantity = item.Quantity, ProductItem = item.Product };
                    lstcartviewmodel.Add(cartitemviewmodel);
                }
                if (lstcartviewmodel.Any())
                {
                    ViewBag.TotalMoney = lstcartviewmodel.Sum(x => x.Quantity * (x.ProductItem.SalePrice > 0 ? x.ProductItem.SalePrice : x.ProductItem.Price));
                }
            }
            return View(lstcartviewmodel);
        }
        public ActionResult Addtocart(int id, int qty)
        {
            var listcartitem = Session["SessionCart"] as List<CartItem>;
            if (listcartitem != null && listcartitem.Any())
            {
                var item = listcartitem.FirstOrDefault(x => x.ProductId == id);
                if (item != null)
                {
                    var updateitem = new CartItem { ProductId = id, Quantity = qty, Product = item.Product };
                    listcartitem.Remove(item);
                    listcartitem.Add(updateitem);
                }
                else
                {
                    var product = db.Products.Find(id);
                    item = new CartItem { ProductId = id, Quantity = qty, Product = product };
                    listcartitem.Add(item);
                }
            }
            else
            {
                listcartitem = new List<CartItem>();
                var product = db.Products.Find(id);
                var item = new CartItem { ProductId = id, Quantity = qty, Product = product };
                listcartitem.Add(item);
            }
            Session["SessionCart"] = listcartitem;
            return ReturnJson(result: 1);
        }
        [HttpPost]
        public ActionResult UpdateCart(List<CartUpdateItem> lstCartUpdateItem)
        {
            List<CartItem> listcartitem = null;
            if (lstCartUpdateItem != null && lstCartUpdateItem.Any())
            {
                listcartitem = new List<CartItem>();
                foreach (var item in lstCartUpdateItem)
                {
                    var product = db.Products.Find(item.ProductId);
                    var cartitem = new CartItem { ProductId = item.ProductId, Quantity = item.Quantity, Product = product };
                    listcartitem.Add(cartitem);
                }
            }
            Session["SessionCart"] = listcartitem;
            List<CartItemViewModel> lstcartviewmodel = null;
            if (listcartitem != null && listcartitem.Any())
            {
                lstcartviewmodel = new List<CartItemViewModel>();
                foreach (var item in listcartitem)
                {
                    var cartitemviewmodel = new CartItemViewModel { Quantity = item.Quantity, ProductItem = item.Product };
                    lstcartviewmodel.Add(cartitemviewmodel);
                }
                if (lstcartviewmodel.Any())
                {
                    ViewBag.TotalMoney = lstcartviewmodel.Sum(x => x.Quantity * (x.ProductItem.SalePrice > 0 && x.ProductItem.IsHasPromotion ? x.ProductItem.SalePrice : x.ProductItem.Price));
                }
            }
            return ReturnJson(result: 1, html: RenderPartialViewToString("UpdateCart", lstcartviewmodel));
        }
        public ActionResult RemoveFromCart(int id)
        {
            try
            {
                var lstcartitem = Session["SessionCart"] as List<CartItem>;
                if (lstcartitem != null)
                {
                    var removeitem = lstcartitem.FirstOrDefault(x => x.ProductId == id);
                    if (removeitem != null)
                    {
                        lstcartitem.Remove(removeitem);
                        Session["SessionCart"] = lstcartitem;
                    }
                    List<CartItemViewModel> lstcartviewmodel = null;
                    if (lstcartitem != null && lstcartitem.Any())
                    {
                        lstcartviewmodel = new List<CartItemViewModel>();
                        foreach (var item in lstcartitem)
                        {
                            var cartitemviewmodel = new CartItemViewModel { Quantity = item.Quantity, ProductItem = item.Product };
                            lstcartviewmodel.Add(cartitemviewmodel);
                        }
                        if (lstcartviewmodel.Any())
                        {
                            ViewBag.TotalMoney = lstcartviewmodel.Sum(x => x.Quantity * (x.ProductItem.SalePrice > 0 && x.ProductItem.IsHasPromotion ? x.ProductItem.SalePrice : x.ProductItem.Price));
                        }
                    }
                    return ReturnJson(result: 1, html: RenderPartialViewToString("UpdateCart", lstcartviewmodel));
                }
                return ReturnJson(result: -1);
            }
            catch (Exception ex)
            {
                return ReturnJson(error: ex.Message);
            }

        }
    }
}