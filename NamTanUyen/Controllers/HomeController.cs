using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;
using System.Data.SqlClient;

namespace NamTanUyen.Controllers
{
    public class HomeController : Controller
    {
        private JewelryContext db = new JewelryContext();
        int _pagesize = 20;
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            var commoninfo = db.CommonInfoes.FirstOrDefault();
            ViewBag.Banners = db.Banners.Where(x => x.IsActive == true).OrderBy(x => x.OrderIndex).ToList();
            ViewBag.News = db.News.Where(x => x.IsActive == true && x.IsHot == true).OrderBy(x=>x.OrderIndex).Take(6);
            ViewBag.ProjectImages = db.ProjectImages.Where(x => x.IsActive == true && x.IsVideo == false).OrderByDescending(x => x.CreatedDate).Take(8);
            ViewBag.ProjectVideos = db.ProjectImages.Where(x => x.IsActive == true && x.IsVideo == true && !string.IsNullOrEmpty(x.VideoUrl)).OrderByDescending(x => x.CreatedDate).Take(8);
            if (commoninfo != null)
            {
                ViewBag.MetaModel = new MetaModel
                {
                    Description = commoninfo.Title,
                    Keywords = commoninfo.MetaKeywords,
                    Title = commoninfo.Title,
                    URL = Request.Url.Host,
                    ImageURL = commoninfo.FacebookImageUrl
                };
            }
            return View();
        }
    }
}