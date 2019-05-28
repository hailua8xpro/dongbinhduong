using Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Filters;
using System.IO;
using NamTanUyen.Extend;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    public class SiteMapController : Controller
    {
        private JewelryContext db = new JewelryContext();
        // GET: cms/SiteMap
        [CustomAuthorizer]
        public ActionResult CreateSiteMap()
        {
            string s = Server.MapPath("~");
            var sitemapItems = new List<SitemapItem>
        {
            new SitemapItem("/tin-tuc",DateTime.Now,SitemapChangeFrequency.Daily,0.8),
            new SitemapItem("/lien-he",DateTime.Now,SitemapChangeFrequency.Yearly,0.5),

        };

            var allobject = db.ProductObjects;
            if (allobject != null && allobject.Any())
            {
                foreach (var item in allobject)
                {
                    sitemapItems.Add(new SitemapItem("/" + item.ProductObjectUrl, DateTime.Now, SitemapChangeFrequency.Daily, 1.0));
                }
            }
            var allcate = db.ProductCategories;
            if (allcate != null && allcate.Any())
            {
                foreach (var item in allcate)
                {
                    sitemapItems.Add(new SitemapItem("/" + item.ProductObject.ProductObjectUrl + "/" + item.ProductCategoryNameUrl, DateTime.Now, SitemapChangeFrequency.Daily, 1.0));
                }
            }
            var lst_all_product = db.Products;
            if (lst_all_product != null)
            {
                foreach (var item in lst_all_product)
                {
                    sitemapItems.Add(new SitemapItem("/" + item.ProductObject.ProductObjectUrl + "/" + item.ProductCategory.ProductCategoryNameUrl + "/" + item.ProductId + "/" + item.ProductNameUrl, DateTime.Now, SitemapChangeFrequency.Daily, 1.0));

                }
            }
            var lst_all_news = db.News;
            if (lst_all_news != null)
            {
                foreach (var item in lst_all_news)
                {
                    sitemapItems.Add(new SitemapItem("/tin-tuc/" + item.NewsUrl + "-" + item.NewsId, DateTime.Now, SitemapChangeFrequency.Monthly, 0.9));
                }

            }
            string filename = "sitemap.xml";
            var fileSavePath = Path.Combine(Server.MapPath("~"), filename);
            var sitemap = SiteMapHelper.GenerateSiteMap(sitemapItems);
            sitemap.Save(fileSavePath);
            return new SitemapResult(sitemapItems);
        }
    }
}