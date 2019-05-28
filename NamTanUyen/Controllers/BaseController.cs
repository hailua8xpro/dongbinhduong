using NamTanUyen.Models;
using NamTanUyen.ViewModels;
using Library.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamTanUyen.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            //Original source code: http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");

            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public JsonResult ReturnJson(string html = null, string error = "", string errorInfo = "", int result = -1)
        {
            return Json(new
            {
                _error = error,
                _errorinfo = errorInfo,
                _result = result,
                _html = html
            }, JsonRequestBehavior.AllowGet);
        }

        public User GetCurrentUser()
        {
            var curr_user = System.Web.HttpContext.Current.Session[ConfigHelper.SessionUser] as User;
            if (curr_user == null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieUser] != null)
                {
                    var userck = JsonConvert.DeserializeObject<LoginViewModel>(System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieUser].Value);
                    if (userck != null && userck.Email != null)
                    {
                        var db = new JewelryContext();
                        curr_user = db.Users.SingleOrDefault(x => x.Email == userck.Email && x.PassWord == userck.Password && x.IsActive == true);
                    }
                }
            }
            return curr_user;
        }
    }
}