using NamTanUyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.ViewModels;

namespace NamTanUyen.Controllers
{
    public class CommonController : BaseController
    {
        private JewelryContext db = new JewelryContext();

        // GET: Common
        public ActionResult Contact()
        {
            var common = db.CommonInfoes.FirstOrDefault();
            ViewBag.MetaModel = new MetaModel
            {
                Description = "Liên hệ",
                Keywords = common.MetaKeywords,
                Title = "Liên hệ",
                URL = Request.Url.Host,
                ImageURL = common.FacebookImageUrl
            };
            return View(common);
        }
        [HttpPost]
        public ActionResult SubmitContact(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var commoninfo = db.CommonInfoes.FirstOrDefault();
                    string[] mailto = null;
                    if (!string.IsNullOrEmpty(commoninfo.EmailTo))
                    {
                        mailto = commoninfo.EmailTo.Split(',');
                    }
                    string subject = "Liên hệ - " + model.FullName;
                    string body = RenderPartialViewToString("_ContactEmail", model);
                    Library.Helper.MailHelper.SendMail(commoninfo.EmailFrom, subject, body, commoninfo.PassWord, 25, "smtp.gmail.com", mailto: mailto);
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
                return ReturnJson(error: errors);
            }
            catch (Exception ex)
            {

                return ReturnJson(error: ex.Message);

            }
        }
        public ActionResult LeftMenu(int objectId = 0, int groupId = 0)
        {
            var objects = db.ProductObjects.Where(x => x.IsActive == true).OrderBy(x => x.OrderIndex);
            ViewBag.ObjectId = objectId;
            ViewBag.GroupId = groupId;
            return PartialView(objects);
        }
        public ActionResult FilterProperty(IEnumerable<FilterItem> filters)
        {
            IEnumerable<int> intqueryarr = null;
            string query = Request["prop"];
            if (!string.IsNullOrEmpty(query))
            {
                var queryarr = query.Split(',');
                if (queryarr != null && queryarr.Length > 0)
                {
                    intqueryarr = queryarr.Select(x => int.Parse(x));
                }
            }
            ViewBag.SelectedProp = intqueryarr;
            ViewBag.Url = Library.Helper.CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page");
            return PartialView(filters);
        }

        public ActionResult Filter(IEnumerable<ProductProperty> lstProp)
        {
            return PartialView();
        }
        public ActionResult ContactInfo()
        {
            var common = db.CommonInfoes.FirstOrDefault();
            return PartialView(common);
        }
        public ActionResult AddViewCount(string url,string note)
        {
            var viewcount = new ViewCount { Url = url,Note=note  };
            db.ViewCounts.Add(viewcount);
            db.SaveChanges();
            return ReturnJson(result: 1);
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