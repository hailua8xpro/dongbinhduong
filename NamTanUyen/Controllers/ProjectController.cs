using NamTanUyen.Models;
using NamTanUyen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamTanUyen.Controllers
{
    public class ProjectController : Controller
    {
        private JewelryContext db = new JewelryContext();

        public ActionResult Image()
        {
            ViewBag.MetaModel = new MetaModel
            {
                Description = "Hình ảnh dự án Khu Dân Cư Nam Tân Uyên",
                Keywords = "Hình ảnh dự án Khu Dân Cư Nam Tân Uyên",
                Title = "Hình ảnh dự án",
                URL = Request.Url.ToString()
            };
            var images = db.ProjectImages.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate);
            return View(images);
        }
        public ActionResult Video()
        {
            ViewBag.MetaModel = new MetaModel
            {
                Description = "Video dự án Khu Dân Cư Nam Tân Uyên",
                Keywords = "Video dự án Khu Dân Cư Nam Tân Uyên",
                Title = "Video dự án",
                URL = Request.Url.ToString()
            };
            var videos = db.ProjectImages.Where(x => x.IsActive == true && x.IsVideo==true).OrderByDescending(x => x.CreatedDate);
            return View(videos);
        }
        public ActionResult About()
        {
            return View();
        }
    }
}