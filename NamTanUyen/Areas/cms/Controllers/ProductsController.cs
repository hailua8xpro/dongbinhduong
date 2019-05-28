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
using System.Data.Entity.Infrastructure.Interception;
using effts;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]

    public class ProductsController : BaseController
    {
        private string _productImageFolder = "/Uploads/images/product/item";
        private JewelryContext db = new JewelryContext();
        int _pagesize = 20;

        // GET: cms/Products
        public ActionResult Index(int page = 1)
        {
            var allproduct = db.Products.Include(p => p.ProductCategory).Include(p => p.ProductGroup);
            IEnumerable<Product> products = null;
            if (page > 1)
            {
                products = allproduct.OrderBy(x => x.OrderIndex).Skip(_pagesize * (page - 1)).Take(_pagesize);
            }
            else
            {
                products = allproduct.OrderBy(x => x.OrderIndex).Take(_pagesize);
            }
            var countallproduct = allproduct.Count();
            var toatalpage = ((float)countallproduct / (float)_pagesize) > (int)(countallproduct / _pagesize)
                ? ((int)(countallproduct / _pagesize)) + 1
                : ((int)(countallproduct / _pagesize));
            ViewBag.Pager = new PagerViewModel
            {
                PageIndex = page,
                PageSize = _pagesize,
                TotalPage = toatalpage,
                Url = CommonHelper.GetUrlWithoutParamInput(Request.Url.ToString(), "page")
            };
            return View(products.ToList());
        }
        public ActionResult Search(string kw, int page = 1)
        {
            if (string.IsNullOrEmpty(kw))
            {
                return Redirect("/");
            }
            kw = kw.Trim();
            ViewBag.MetaModel = new MetaModel
            {
                Title = "Kết quả tìm kiếm với từ khóa " + kw
            };
            kw = kw.Replace(" ", " AND ");
            DbInterception.Add(new FtsInterceptor());
            IEnumerable<Product> result = null;
            var search = db.Products.FreeTextSearch(x => x.ProductName, kw);
            if (search != null && search.Any())
            {
                result = search.ToList();
                if (page > 1)
                {
                    result = search.OrderBy(x => x.OrderIndex).Skip(_pagesize * (page - 1)).Take(_pagesize);
                }
                else
                {
                    result = search.OrderBy(x => x.OrderIndex).Take(_pagesize);
                }
                var countallproduct = search.Count();
                var toatalpage = ((float)countallproduct / (float)_pagesize) > (int)(countallproduct / _pagesize)
                    ? ((int)(countallproduct / _pagesize)) + 1
                    : ((int)(countallproduct / _pagesize));

            }
            return View(result);
        }
        // GET: cms/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: cms/Products/Create
        public ActionResult Create()
        {
            var objects = db.ProductObjects.Where(x => x.IsActive == true).OrderBy(x => x.OrderIndex);
            ViewBag.ProductObjectId = new SelectList(objects, "ProductObjectId", "ProductObjectName");
            IEnumerable<ProductCategory> cates = db.ProductCategories.Where(x => x.ProductObjectId == 0);
            ViewBag.ProductCategoryID = new SelectList(cates, "ProductCategoryId", "ProductCategoryName");
            IEnumerable<ProductGroup> groups = db.ProductGroups.Where(x => x.ProductCategoryId == 0);
            ViewBag.ProductGroupID = new SelectList(groups, "ProductGroupId", "ProductGroupName");
            ViewBag.SubCate = db.ProductCategories.Where(x => x.IsMainCate == false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                product.ProductNameUrl = CommonHelper.BuildUrl(product.ProductName);
                product.CreatedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, product.ProductNameUrl, _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        product.ImageUrl = imageurl;
                    }
                }
                db.Products.Add(product);
                db.SaveChanges();
                var strpg = Request.Form["GroupProductList"];
                if (!string.IsNullOrEmpty(strpg))
                {
                    var lstgc = JsonConvert.DeserializeObject<IEnumerable<ProductProductGroup>>(strpg);
                    if (lstgc != null && lstgc.Any())
                    {

                        foreach (var item1 in lstgc)
                        {
                            var pg = new ProductProductGroup { ProductId = product.ProductId, ProductGroupId = item1.ProductGroupId, ProductCategoryId = item1.ProductCategoryId };
                            db.Product_ProductGroups.Add(pg);
                        }
                        db.SaveChanges();
                    }
                }
                var param = new SqlParameter[] {
                new SqlParameter {ParameterName="Mode",Value=1 },
                new SqlParameter {ParameterName="Id",Value=product.ProductId },
                new SqlParameter {ParameterName="OldValue",Value="" },
                new SqlParameter{ParameterName="NewValue",Value="" },
            };
                //db.Database.ExecuteSqlCommand("Product_UpdateListPropertyValue @Mode,@Id,@OldValue,@NewValue", param);
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

        // GET: cms/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductObjectId = new SelectList(db.ProductObjects, "ProductObjectId", "ProductObjectName", product.ProductObjectId);
            ViewBag.ProductCategoryId = new SelectList(product.ProductObject.ProductCategory, "ProductCategoryId", "ProductCategoryName", product.ProductCategoryId);
            ViewBag.ProductGroupId = new SelectList(product.ProductCategory.ProductGroup, "ProductGroupId", "ProductGroupName", product.ProductGroupId);
            return View(product);
        }

        // POST: cms/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                product.ProductNameUrl = CommonHelper.BuildUrl(product.ProductName);
                product.ModifiedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, product.ProductNameUrl, _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        product.ImageUrl = imageurl;
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                var strpg = Request.Form["GroupProductList"];
                if (!string.IsNullOrEmpty(strpg))
                {
                    var lstgc = JsonConvert.DeserializeObject<IEnumerable<ProductProductGroup>>(strpg);
                    if (lstgc != null && lstgc.Any())
                    {
                        db.Database.ExecuteSqlCommand("delete from Product_ProductGroup where ProductId={0}", product.ProductId);
                        foreach (var item1 in lstgc)
                        {
                            var pg = new ProductProductGroup { ProductId = product.ProductId, ProductGroupId = item1.ProductGroupId, ProductCategoryId = item1.ProductCategoryId };
                            db.Product_ProductGroups.Add(pg);
                        }
                        db.SaveChanges();
                    }
                }
                var param = new SqlParameter[] {
                new SqlParameter {ParameterName="Mode",Value=1 },
                new SqlParameter {ParameterName="Id",Value=product.ProductId },
                new SqlParameter {ParameterName="OldValue",Value="" },
                new SqlParameter{ParameterName="NewValue",Value="" },
            };
                //db.Database.ExecuteSqlCommand("Product_UpdateListPropertyValue @Mode,@Id,@OldValue,@NewValue", param);
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

        // GET: cms/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: cms/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
