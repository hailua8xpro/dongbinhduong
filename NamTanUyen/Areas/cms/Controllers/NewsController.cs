using Library.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Filters;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;

namespace NamTanUyen.Areas.cms.Controllers
{

    [CustomAuthorizer]
    public class NewsController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        int _pagesize = 20;
        private string _newsImageFolder = "/Uploads/images/news";
        // GET: cms/News
        public ActionResult Index(int page = 1)
        {
            var allnews = db.News;
            IEnumerable<News> lstnews = null;
            if (page > 1)
            {
                lstnews = allnews.OrderByDescending(x => x.CreatedDate).Skip(_pagesize * (page - 1)).Take(_pagesize);
            }
            else
            {
                lstnews = allnews.OrderByDescending(x => x.CreatedDate).Take(_pagesize);
            }
            var countallnews = allnews.Count();
            var toatalpage = ((float)countallnews / (float)_pagesize) > (int)(countallnews / _pagesize)
                ? ((int)(countallnews / _pagesize)) + 1
                : ((int)(countallnews / _pagesize));
            ViewBag.Pager = new PagerViewModel
            {
                PageIndex = page,
                PageSize = _pagesize,
                TotalPage = toatalpage,
                Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
            };
            return View(lstnews.ToList());
        }

        // GET: cms/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: cms/News/Create
        public ActionResult Create()
        {
            ViewBag.NewsCategoryId = new SelectList(db.NewsCategories, "NewsCategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News news, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                news.CreatedUserId = CurrentUser.AdminUserId;
                news.NewsUrl = CommonHelper.BuildUrl(news.Title);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, news.NewsUrl, _newsImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        news.ImageUrl = imageurl;
                    }
                }
                db.News.Add(news);
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

        // GET: cms/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsCategoryId = new SelectList(db.NewsCategories, "NewsCategoryId", "CategoryName", news.NewsCategoryId);
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News news, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                news.ModifiedUserId = CurrentUser.AdminUserId;
                news.NewsUrl = CommonHelper.BuildUrl(news.Title);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, news.NewsUrl, _newsImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        news.ImageUrl = imageurl;
                    }
                }
                db.Entry(news).State = EntityState.Modified;
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
        // GET: cms/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: cms/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
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
