using Library.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;

namespace NamTanUyen.Areas.cms.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            CurrentUser = GetCurrentUser();
        }
        public AdminUser CurrentUser { get; }
        public string SystemErrorMessage = "Có lỗi xảy ra, vui lòng thử lại";
        private JewelryContext db = new JewelryContext();
        public bool IsLogged { get { return CheckLogged(); } }
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
        public bool UploadImage(ref string error, ref string imageUrl, HttpPostedFileBase image, string modelName, string imageFolder)
        {
            try
            {
                if (image.ContentLength <= 0 || image.ContentLength >= 3000000)
                {
                    error = "Vui lòng chọn file có kích thước nhỏ hơn 3 Mb";
                    return false;
                }
                var extension = Path.GetExtension(image.FileName);
                var server_path = Server.MapPath("~" + imageFolder + "/" + modelName);
                string imagename = Guid.NewGuid().ToString();
                if (!Directory.Exists(server_path))
                {
                    Directory.CreateDirectory(server_path);
                }
                string file_name = imagename + extension;
                var fileSavePath = Path.Combine(server_path, file_name);
                if (!string.IsNullOrEmpty(modelName))
                {
                    imageUrl = imageFolder + "/" + modelName + "/" + file_name;
                }
                else
                {
                    imageUrl = imageFolder + "/" + file_name;
                }
                image.SaveAs(fileSavePath);
                return true;
            }
            catch
            {
                error = "Có lỗi xảy ra, vui lòng thử lại";
                return false;
            }
        }
        public bool UploadFile(ref string error, ref string fileUrl, HttpPostedFileBase file, string modelName, string fileFolder, int maxLength = 3000000)
        {
            try
            {
                if (file.ContentLength <= 0 || file.ContentLength >= maxLength)
                {
                    error = "Vui lòng chọn file có kích thước nhỏ hơn " + maxLength + " byte";
                    return false;
                }
                var extension = Path.GetExtension(file.FileName);
                var server_path = Server.MapPath("~" + fileFolder + "/" + modelName);
                string filename = Guid.NewGuid().ToString();
                if (!Directory.Exists(server_path))
                {
                    Directory.CreateDirectory(server_path);
                }
                string file_name = filename + extension;
                var fileSavePath = Path.Combine(server_path, file_name);
                if (!string.IsNullOrEmpty(modelName))
                {
                    fileUrl = fileFolder + "/" + modelName + "/" + file_name;
                }
                else
                {
                    fileUrl = fileFolder + "/" + file_name;
                }
                file.SaveAs(fileSavePath);
                return true;
            }
            catch
            {
                error = "Có lỗi xảy ra, vui lòng thử lại";
                return false;
            }
        }
        public string GetModelError(ICollection<ModelState> errors)
        {
            string result = "";
            foreach (ModelState modelState in errors)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    result += error.ErrorMessage + Environment.NewLine;
                }
            }
            return result;
        }
        public bool CheckLogged()
        {
            bool islogged = false;
            var curr_session = System.Web.HttpContext.Current.Session[ConfigHelper.SessionAdminUser];
            if (curr_session != null)
            {
                islogged = true;
            }
            else if (System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieAdmin] != null)
            {
                var userck = JsonConvert.DeserializeObject<LoginViewModel>(System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieAdmin].Value);
                if (userck != null && userck.Email != null)
                {
                    var db_user = db.AdminUsers.SingleOrDefault(x => x.Email == userck.Email && x.PassWord == userck.Password && x.IsActive == true);
                    if (db_user != null)
                    {
                        islogged = true;
                    }
                }
            }
            return islogged;
        }
        public AdminUser GetCurrentUser()
        {
            var curr_user = System.Web.HttpContext.Current.Session[ConfigHelper.SessionAdminUser] as AdminUser;
            if (curr_user == null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieAdmin] != null)
                {
                    var userck = JsonConvert.DeserializeObject<LoginViewModel>(System.Web.HttpContext.Current.Request.Cookies[ConfigHelper.CookieAdmin].Value);
                    if (userck != null && userck.Email != null)
                    {
                        curr_user = db.AdminUsers.SingleOrDefault(x => x.Email == userck.Email && x.PassWord == userck.Password && x.IsActive == true);
                    }
                }
            }
            return curr_user;
        }
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
    }
}