using NamTanUyen.Models;
using NamTanUyen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamTanUyen.Controllers
{
    public class StaticPageController : Controller
    {
        private JewelryContext db = new JewelryContext();

        // GET: StaticPage
        public ActionResult Index(string pageUrl)
        {
            var page = db.StaticPages.FirstOrDefault(x => x.PageUrl == pageUrl);
            if (page == null)
            {
                return HttpNotFound("404");
            }
            ViewBag.MetaModel = new MetaModel
            {
                Description = page.MetaDescription,
                Keywords = page.MetaKeyword,
                Title = page.Title,
                URL = Request.Url.Host
            };
            return View(page);
        }
    }
}