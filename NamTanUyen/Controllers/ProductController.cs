using effts;
using Library.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using NamTanUyen.Models;
using NamTanUyen.ViewModels;

namespace NamTanUyen.Controllers
{
    public class ProductController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        int _pagesize = 20;
        // GET: Product
        public ActionResult Detail(int productId)
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound("404");
            }
            ViewBag.MetaModel = new MetaModel
            {
                Description = product.MetaDescription,
                Keywords = product.Metakeywords,
                Title = product.ProductName,
                URL = Request.Url.ToString(),
                ImageURL = string.IsNullOrEmpty(product.ImageUrl) ? string.Empty : product.ImageUrl.TrimStart('/')
            };
            ViewBag.RelatestProduct = db.Products.Where(x => x.ProductGroupId == product.ProductGroupId && product.ProductId != x.ProductId && x.IsActive == true).Take(3);
            ViewBag.LatestNews = db.News.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate).Take(4);
            ViewBag.ObjectId = product.ProductObjectId;
            return View(product);
        }
        public ActionResult Search(string kw, string pr, string a, string prop, int page = 1)
        {
            if (string.IsNullOrEmpty(kw))
            {
                return Redirect("/");
            }
            prop = prop ?? "";
            kw = kw.Trim();
            ViewBag.MetaModel = new MetaModel
            {
                Title = "Kết quả tìm kiếm với từ khóa " + kw
            };
            var param = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=0 },
                new SqlParameter {ParameterName="ProductGroupId",Value=0 },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=0 },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value=kw },
                new SqlParameter {ParameterName="PageNumber",Value=page },
                new SqlParameter {ParameterName="RowspPage",Value=_pagesize },
            };
            var products = db.Database.SqlQuery<ProductItem>("Product_GetProduct @ProductObjectId,@ProductGroupId,@ListPropertyDetailId,@ProductCategoryId,@BrandId,@ProductName,@PageNumber,@RowspPage", param).ToList();
            var param1 = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=0 },
                new SqlParameter {ParameterName="ProductGroupId",Value=0 },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=0 },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value=kw },
            };
            var filters = db.Database.SqlQuery<FilterItem>("Product_GetFilter @ProductName,@ProductCategoryId,@ProductGroupId,@ProductObjectId,@BrandId,@ListPropertyDetailId", param1).ToList();
            ViewBag.Filters = filters;
            decimal totalpage = 0;
            if (products != null && products.Any())
            {
                totalpage = products.FirstOrDefault().TotalPage;
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = (int)totalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }

            IEnumerable<ProductItem> result = null;
            ViewBag.SortText = "Sắp theo";
            if (products != null && products.Any())
            {
                result = products;
                if (pr == "asc")
                {
                    ViewBag.SortText = "Giá tăng dần";
                    result = products.OrderBy(x => x.Price);
                }
                if (pr == "desc")
                {
                    ViewBag.SortText = "Giá giảm dần";
                    result = products.OrderByDescending(x => x.Price);
                }
                if (a == "desc")
                {
                    ViewBag.SortText = "Mới nhất";
                    result = products.OrderByDescending(x => x.CreatedDate);
                }
            }
            return View(result);
        }
        public ActionResult ProductObject(string objectUrl, string pr, string a, string prop, int page = 1)
        {
            var productObject = db.ProductObjects.FirstOrDefault(x => x.ProductObjectUrl == objectUrl && x.IsActive == true);
            if (productObject == null)
            {
                return HttpNotFound("404");
            }
            prop = prop ?? "";
            ViewBag.Object = productObject;
            ViewBag.ObjectId = productObject.ProductObjectId;
            var param = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=productObject.ProductObjectId },
                new SqlParameter {ParameterName="ProductGroupId",Value=0 },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=0 },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value="" },
                new SqlParameter {ParameterName="PageNumber",Value=page },
                new SqlParameter {ParameterName="RowspPage",Value=_pagesize },
            };
            var products = db.Database.SqlQuery<ProductItem>("Product_GetProduct @ProductObjectId,@ProductGroupId,@ListPropertyDetailId,@ProductCategoryId,@BrandId,@ProductName,@PageNumber,@RowspPage", param).ToList();
            var param1 = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=productObject.ProductObjectId },
                new SqlParameter {ParameterName="ProductGroupId",Value=0 },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=0 },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value="" },
            };
            var filters = db.Database.SqlQuery<FilterItem>("Product_GetFilter @ProductName,@ProductCategoryId,@ProductGroupId,@ProductObjectId,@BrandId,@ListPropertyDetailId", param1).ToList();
            ViewBag.Filters = filters;
            ViewBag.SortText = "Sắp theo";
            decimal totalpage = 0;
            if (products != null && products.Any())
            {
                totalpage = products.FirstOrDefault().TotalPage;
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = (int)totalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }

            IEnumerable<ProductItem> result = null;
            ViewBag.SortText = "Sắp theo";
            if (products != null && products.Any())
            {
                result = products;
                if (pr == "asc")
                {
                    ViewBag.SortText = "Giá tăng dần";
                    result = products.OrderBy(x => x.Price);
                }
                if (pr == "desc")
                {
                    ViewBag.SortText = "Giá giảm dần";
                    result = products.OrderByDescending(x => x.Price);
                }
                if (a == "desc")
                {
                    ViewBag.SortText = "Mới nhất";
                    result = products.OrderByDescending(x => x.CreatedDate);
                }
            }
            return View(result);
        }
        public ActionResult ProductCategory(string objectUrl, string cateUrl, string pr, string a, string prop, int page = 1)
        {
            var productCate = db.ProductCategories.FirstOrDefault(x => x.ProductCategoryNameUrl == cateUrl && x.IsActive == true);
            if (productCate == null)
            {
                return HttpNotFound("404");
            }
            prop = prop ?? "";
            ViewBag.Cate = productCate;
            ViewBag.ObjectId = productCate.ProductObjectId;
            var param = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=productCate.ProductObjectId },
                new SqlParameter {ParameterName="ProductGroupId",Value=0 },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=productCate.ProductCategoryId },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value="" },
                new SqlParameter {ParameterName="PageNumber",Value=page },
                new SqlParameter {ParameterName="RowspPage",Value=_pagesize },
            };
            var products = db.Database.SqlQuery<ProductItem>("Product_GetProduct @ProductObjectId,@ProductGroupId,@ListPropertyDetailId,@ProductCategoryId,@BrandId,@ProductName,@PageNumber,@RowspPage", param).ToList();
            var param1 = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=productCate.ProductObjectId },
                new SqlParameter {ParameterName="ProductGroupId",Value=0 },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=productCate.ProductCategoryId },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value="" },
            };
            var filters = db.Database.SqlQuery<FilterItem>("Product_GetFilter @ProductName,@ProductCategoryId,@ProductGroupId,@ProductObjectId,@BrandId,@ListPropertyDetailId", param1).ToList();
            ViewBag.Filters = filters;
            ViewBag.SortText = "Sắp theo";
            decimal totalpage = 0;
            if (products != null && products.Any())
            {
                totalpage = products.FirstOrDefault().TotalPage;
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = (int)totalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }

            IEnumerable<ProductItem> result = null;
            ViewBag.SortText = "Sắp theo";
            if (products != null && products.Any())
            {
                result = products;
                if (pr == "asc")
                {
                    ViewBag.SortText = "Giá tăng dần";
                    result = products.OrderBy(x => x.Price);
                }
                if (pr == "desc")
                {
                    ViewBag.SortText = "Giá giảm dần";
                    result = products.OrderByDescending(x => x.Price);
                }
                if (a == "desc")
                {
                    ViewBag.SortText = "Mới nhất";
                    result = products.OrderByDescending(x => x.CreatedDate);
                }
            }
            return View(result);
        }
        public ActionResult ProductGroup(string objectUrl, string cateUrl, string groupUrl, string pr, string a, string prop, int page = 1)
        {
            var productGroup = db.ProductGroups.FirstOrDefault(x => x.ProductGroupUrl == groupUrl && x.ProductCategory.ProductCategoryNameUrl == cateUrl && x.IsActive == true);
            if (productGroup == null)
            {
                return HttpNotFound("404");
            }
            prop = prop ?? "";
            ViewBag.Group = productGroup;
            ViewBag.ObjectId = productGroup.ProductObjectId;
            var param = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=productGroup.ProductObjectId },
                new SqlParameter {ParameterName="ProductGroupId",Value=productGroup.ProductGroupId },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=productGroup.ProductCategoryId },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value="" },
                new SqlParameter {ParameterName="PageNumber",Value=page },
                new SqlParameter {ParameterName="RowspPage",Value=_pagesize },
            };
            var products = db.Database.SqlQuery<ProductItem>("Product_GetProduct @ProductObjectId,@ProductGroupId,@ListPropertyDetailId,@ProductCategoryId,@BrandId,@ProductName,@PageNumber,@RowspPage", param).ToList();
            var param1 = new SqlParameter[] {
                new SqlParameter {ParameterName="ProductObjectId",Value=0 },
                new SqlParameter {ParameterName="ProductGroupId",Value=productGroup.ProductGroupId },
                new SqlParameter {ParameterName="ListPropertyDetailId",Value=prop },
                new SqlParameter{ParameterName="ProductCategoryId",Value=0 },
                new SqlParameter{ParameterName="BrandId",Value=0 },
                new SqlParameter {ParameterName="ProductName",Value="" },
            };
            var filters = db.Database.SqlQuery<FilterItem>("Product_GetFilter @ProductName,@ProductCategoryId,@ProductGroupId,@ProductObjectId,@BrandId,@ListPropertyDetailId", param1).ToList();
            ViewBag.Filters = filters;
            decimal totalpage = 0;
            if (products != null && products.Any())
            {
                totalpage = products.FirstOrDefault().TotalPage;
                ViewBag.Pager = new PagerViewModel
                {
                    PageIndex = page,
                    PageSize = _pagesize,
                    TotalPage = (int)totalpage,
                    Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
                };
            }

            IEnumerable<ProductItem> result = null;
            ViewBag.SortText = "Sắp theo";
            if (products != null && products.Any())
            {
                result = products;
                if (pr == "asc")
                {
                    ViewBag.SortText = "Giá tăng dần";
                    result = products.OrderBy(x => x.Price);
                }
                if (pr == "desc")
                {
                    ViewBag.SortText = "Giá giảm dần";
                    result = products.OrderByDescending(x => x.Price);
                }
                if (a == "desc")
                {
                    ViewBag.SortText = "Mới nhất";
                    result = products.OrderByDescending(x => x.CreatedDate);
                }
            }
            return View(result);
        }

        public ActionResult HotProduct()
        {
            var products = db.Products.Where(x => x.IsBestSell == true).Take(4);
            return PartialView(products);
        }
    }
}