using AutoMapper;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;
using Library.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamTanUyen.Controllers
{
    public class AccountController : BaseController
    {
        JewelryContext db = new JewelryContext();
        int _pagesize = 20;
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //get user in db
                var password = CryptoHelper.Encrypt(model.Password, model.Email);
                var user = db.Users.FirstOrDefault(x => x.Email == model.Email && x.PassWord == password && x.IsActive == true);
                if (user == null)
                {
                    return ReturnJson(error: "Email đăng nhập hoặc mật khẩu không đúng");
                }
                Session[ConfigHelper.SessionUser] = user;
                if (model.IsRemember)
                {
                    var userviewmodel = new LoginViewModel { Email = model.Email, Password = password, IsRemember = true };
                    if (System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieUser] != null)
                    {
                        CookieHelper.EditCookie(ConfigHelper.CookieUser, JsonConvert.SerializeObject(userviewmodel), DateTime.Now.AddMonths(1));
                    }
                    else
                    {
                        CookieHelper.AddCookie(ConfigHelper.CookieUser, JsonConvert.SerializeObject(userviewmodel), DateTime.Now.AddMonths(1));
                    }

                }
                return ReturnJson(result: 1);
            }
            return ReturnJson(error: "Email đăng nhập hoặc mật khẩu không đúng");

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(x => x.Email == model.Email);
                if (check != null)
                {
                    return ReturnJson(error: "Email đã tồn tại");
                }
                model.Password = CryptoHelper.Encrypt(model.Password, model.Email);
                var user = new User { Email = model.Email, IsActive = true, PassWord = model.Password };
                db.Users.Add(user);
                db.SaveChanges();
                return ReturnJson(result: 1);
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
        public ActionResult ForgotPass()
        {

            ViewBag.MetaModel = new MetaModel
            {
                Title = "Quên mật khẩu",
                Description = "Quên mật khẩu"
            };
            return View();
        }
        [HttpPost]
        public ActionResult ResetPass(string email)
        {
            try
            {
                var user = db.Users.FirstOrDefault(x => x.Email == email && x.IsActive == true);
                if (user == null)
                {
                    return ReturnJson("Email này chưa được đăng ký");
                }
                var pass = CommonHelper.GetRandomString(6);
                user.PassWord = CryptoHelper.Encrypt(pass, email);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                string subject = "Mật khẩu đăng nhập tại VTFashion";
                string body = string.Format("<p>Mật khẩu mới của bạn:<strong> {0}</strong></p>", pass);
                MailHelper.SendMail("hailua8xpro@gmail.com", subject, body, "suckhoelaso1", 25, "smtp.gmail.com", mailto: email);
                return ReturnJson(result: 1);
            }
            catch (Exception)
            {
                return ReturnJson();
            }
        }
        public ActionResult ChangePassword()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return Redirect("/");
            }
            ViewBag.MetaModel = new MetaModel
            {
                Title = "Đổi mật khẩu",
                Description = "Đổi mật khẩu"
            };
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UpdatePassViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = GetCurrentUser();
                    if (user == null)
                    {
                        return ReturnJson("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại");
                    }
                    else if (CryptoHelper.Encrypt(model.CurrentPass, user.Email) != user.PassWord)
                        return ReturnJson("Password không đúng");
                    else if (model.NewsPass != model.NewPassConfirm)
                        return ReturnJson("Xác nhận mật khẩu không đúng");
                    var currentUserdb = db.Users.SingleOrDefault(x => x.Email == user.Email);
                    currentUserdb.PassWord = CryptoHelper.Encrypt(model.NewsPass, user.Email);
                    db.Entry(currentUserdb).State = EntityState.Modified;
                    db.SaveChanges();
                    return ReturnJson(result: 1);
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
            catch (Exception ex)
            {
                return ReturnJson(ex.Message);
            }
        }
        public ActionResult Signout()
        {
            Session[ConfigHelper.SessionUser] = null;
            CookieHelper.DeleteCookie(ConfigHelper.CookieUser, "");
            return ReturnJson(result: 1);
        }
        public ActionResult AddWishlist(int id)
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return ReturnJson(error: "Bạn cần đăng nhập để thực hiện chức năng này");
            }
            var w = db.Wishlists.FirstOrDefault(x => x.UserId == user.UserId && x.ProductId == id);
            if (w != null)
            {
                return ReturnJson(error: "Sản phẩm đã tồn tại trong WishList");
            }
            var wi = new Wishlist { ProductId = id, UserId = user.UserId };
            db.Wishlists.Add(wi);
            db.SaveChanges();
            return ReturnJson(result: 1);
        }
        public ActionResult GetWishList()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return ReturnJson();
            }
            var wl = db.Wishlists.Where(x => x.UserId == user.UserId);
            IEnumerable<int> lstid = null;
            if (wl != null && wl.Any())
            {
                lstid = wl.Select(x => x.ProductId);
            }
            return Json(lstid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WishList(int page = 1)
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return Redirect("/");
            }
            ViewBag.MetaModel = new MetaModel
            {
                Title = "Sản phẩm yêu thích",
                Description = "Sản phẩm yêu thích"
            };
            var product = db.Wishlists.Where(x => x.UserId == user.UserId).Select(y => y.Product);
            if (product != null && product.Any())
            {
                var countallproduct = product.Count();
                var toatalpage = ((float)countallproduct / (float)_pagesize) > (int)(countallproduct / _pagesize)
                    ? ((int)(countallproduct / _pagesize)) + 1
                    : ((int)(countallproduct / _pagesize));
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = (int)toatalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }
            return View(product);
        }

        public ActionResult CheckIfWishListItemExisted(int id)
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return ReturnJson();
            }
            var item = db.Wishlists.FirstOrDefault(x => x.ProductId == id && x.UserId == user.UserId);
            if (item == null)
            {
                return ReturnJson();
            }
            return ReturnJson(result: 1);
        }
        public ActionResult MyAccount()
        {
            var user = GetCurrentUser();

            if (user == null)
            {
                return Redirect("/");
            }
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<User, MyAccountViewModel>());
            var usermodel = Mapper.Map<MyAccountViewModel>(user);
            ViewBag.MetaModel = new MetaModel
            {
                Title = "Tài khoản",
                Description = "Tài khoản"
            };
            return View(usermodel);
        }
        [HttpPost]
        public ActionResult UpdateMyAccount(MyAccountViewModel model)
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return ReturnJson("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại");
            }
            if (ModelState.IsValid)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<MyAccountViewModel, User>());
                user = Mapper.Map(model, user);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return ReturnJson(result: 1);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}