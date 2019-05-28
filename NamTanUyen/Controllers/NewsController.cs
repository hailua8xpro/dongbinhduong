using Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;

namespace NamTanUyen.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private JewelryContext db = new JewelryContext();
        int _pagesize = 20;

        public ActionResult Index(int page = 1)
        {
            ViewBag.MetaModel = new MetaModel
            {
                Description = "Tin tức" + (page > 1 ? " | trang " + page : string.Empty),
                Keywords = "Tin tức" + (page > 1 ? " | trang " + page : string.Empty),
                Title = "Tin tức" + (page > 1 ? " | trang " + page : string.Empty),
                URL = "tin-tuc" + (page > 1 ? "?page=" + page : string.Empty)
            };
            var allnews = db.News.Where(x => x.IsActive == true && x.NewsCategory.IsActive == true);
            IEnumerable<News> newss = null;
            if (allnews != null && allnews.Any())
            {
                if (page > 1)
                {
                    newss = allnews.OrderByDescending(x => x.CreatedDate).Skip(_pagesize * (page - 1)).Take(_pagesize);
                }
                else
                {
                    newss = allnews.OrderByDescending(x => x.CreatedDate).Take(_pagesize);
                }
                var countallproduct = allnews.Count();
                var totalpage = ((float)countallproduct / (float)_pagesize) > (int)(countallproduct / _pagesize)
                    ? ((int)(countallproduct / _pagesize)) + 1
                    : ((int)(countallproduct / _pagesize));
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = totalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }
            ViewBag.HotNews = db.News.Where(x => x.IsHot == true && x.IsActive == true).OrderByDescending(x => x.CreatedDate).Take(4);
            return View(newss);
        }
        public ActionResult Category(string cateUrl, int page = 1)
        {
            var cate = db.NewsCategories.FirstOrDefault(x => x.CategoryUrl == cateUrl && x.IsActive == true);
            if (cate == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cate = cate;
            ViewBag.MetaModel = new MetaModel
            {
                Description = cate.CategoryName + (page > 1 ? " | trang " + page : string.Empty),
                Keywords = cate.CategoryName + (page > 1 ? " | trang " + page : string.Empty),
                Title = cate.CategoryName + (page > 1 ? " | trang " + page : string.Empty),
                URL = "tin-tuc/" + cate.CategoryUrl + (page > 1 ? "?page=" + page : string.Empty)
            };
            var allnews = cate.News.Where(x => x.IsActive == true);
            IEnumerable<News> newss = null;
            if (allnews != null && allnews.Any())
            {
                if (page > 1)
                {
                    newss = allnews.OrderByDescending(x => x.CreatedDate).Skip(_pagesize * (page - 1)).Take(_pagesize);
                }
                else
                {
                    newss = allnews.OrderByDescending(x => x.CreatedDate).Take(_pagesize);
                }
                var countallproduct = allnews.Count();
                var totalpage = ((float)countallproduct / (float)_pagesize) > (int)(countallproduct / _pagesize)
                    ? ((int)(countallproduct / _pagesize)) + 1
                    : ((int)(countallproduct / _pagesize));
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = totalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }
            return View(newss);
        }
        public ActionResult LatestNews()
        {
            var news = db.NewsCategories.OrderBy(x => x.OrderIndex);
            return PartialView(news);
        }
        public ActionResult Detail(int newsId)
        {
            var news = db.News.Find(newsId);
            if (news == null)
            {
                return HttpNotFound("404");
            }
            ViewBag.NewsCategoryId = news.NewsCategoryId;
            ViewBag.MetaModel = new MetaModel
            {
                Description = news.MetaDescription,
                Keywords = news.MetaKeywords,
                Title = news.Title,
                URL = Request.Url.ToString(),
                ImageURL = news.ImageUrl

            };
            ViewBag.HotNews = db.News.Where(x => x.IsActive == true && x.IsHot==true && x.NewsId != newsId).OrderBy(x => x.OrderIndex).Take(4);
            return View(news);
        }
    }
}