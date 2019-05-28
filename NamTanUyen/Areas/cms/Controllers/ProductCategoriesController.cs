using Library.Helper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Filters;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]

    public class ProductCategoriesController : BaseController
    {
        JewelryContext db = new JewelryContext();
        private string _productImageFolder = "/Uploads/images/product/category";
        // GET: cms/ProductCategories
        public ActionResult Index()
        {
            return View(db.ProductCategories.ToList());
        }

        // GET: cms/ProductCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: cms/ProductCategories/Create
        public ActionResult Create()
        {
            ViewBag.ProductObjectId = new SelectList(db.ProductObjects, "ProductObjectId", "ProductObjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProductCategory productCategory, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                productCategory.ProductCategoryNameUrl = CommonHelper.BuildUrl(productCategory.ProductCategoryName);
                productCategory.CreatedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productCategory.ImageUrl = imageurl;
                    }
                }
                db.ProductCategories.Add(productCategory);
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
        // GET: cms/ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductObjectId = new SelectList(db.ProductObjects, "ProductObjectId", "ProductObjectName", productCategory.ProductObjectId);
            return View(productCategory);
        }

        // POST: cms/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productCategory, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                productCategory.ModifiedUserId = CurrentUser.ModifiedUserId;
                productCategory.ProductCategoryNameUrl = CommonHelper.BuildUrl(productCategory.ProductCategoryName);
                productCategory.ModifiedDate = DateTime.Now;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productCategory.ImageUrl = imageurl;
                    }
                }
                db.Entry(productCategory).State = EntityState.Modified;
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

        // GET: cms/ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: cms/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetMainCateDropdownByObjectId(int objectid)
        {
            var cates = db.ProductCategories.Where(x => x.ProductObjectId == objectid && x.IsMainCate == true);
            return PartialView(cates);
        }
        public ActionResult GetCateDropdownByObjectId(int objectid)
        {
            var cates = db.ProductCategories.Where(x => x.ProductObjectId == objectid);
            return PartialView(cates);
        }
        public ActionResult GetListCateByObjectId(int objectId)
        {
            var cates = db.ProductCategories.Where(x => x.ProductObjectId == objectId && x.IsMainCate == false);
            return PartialView(cates);
        }
        public ActionResult GetListCateByProductId(int objectId, int productId)
        {
            var cates = db.ProductCategories.Where(x => x.ProductObjectId == objectId && x.IsMainCate == false);
            ViewBag.ProductGroup = db.Product_ProductGroups.Where(x => x.ProductId == productId);
            return PartialView(cates);
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
