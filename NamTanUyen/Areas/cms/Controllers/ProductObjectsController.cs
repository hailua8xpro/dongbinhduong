using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;
using Library.Helper;

namespace NamTanUyen.Areas.cms.Controllers
{
    public class ProductObjectsController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _productImageFolder = "/Uploads/images/product/object";
        // GET: cms/ProductObjects
        public ActionResult Index()
        {
            return View(db.ProductObjects.ToList());
        }

        // GET: cms/ProductObjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductObject productObject = db.ProductObjects.Find(id);
            if (productObject == null)
            {
                return HttpNotFound();
            }
            return View(productObject);
        }

        // GET: cms/ProductObjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cms/ProductObjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductObject productObject, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageBannerFile)
        {
            if (ModelState.IsValid)
            {
                productObject.ProductObjectUrl = CommonHelper.BuildUrl(productObject.ProductObjectName);
                productObject.CreatedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productObject.ImageUrl = imageurl;
                    }
                }
                if (ImageBannerFile != null)
                {
                    if (ImageBannerFile != null && ImageBannerFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageBannerFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productObject.BannerImageUrl = imageurl;
                    }
                }
                db.ProductObjects.Add(productObject);
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

        // GET: cms/ProductObjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductObject productObject = db.ProductObjects.Find(id);
            if (productObject == null)
            {
                return HttpNotFound();
            }
            return View(productObject);
        }

        // POST: cms/ProductObjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductObject productObject, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageBannerFile)
        {
            if (ModelState.IsValid)
            {
                productObject.ModifiedUserId = CurrentUser.ModifiedUserId;
                productObject.ProductObjectUrl = CommonHelper.BuildUrl(productObject.ProductObjectName);
                productObject.ModifiedDate = DateTime.Now;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productObject.ImageUrl = imageurl;
                    }
                }
                if (ImageBannerFile != null)
                {
                    if (ImageBannerFile != null && ImageBannerFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageBannerFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productObject.BannerImageUrl = imageurl;
                    }
                }
                db.Entry(productObject).State = EntityState.Modified;
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

        // GET: cms/ProductObjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductObject productObject = db.ProductObjects.Find(id);
            if (productObject == null)
            {
                return HttpNotFound();
            }
            return View(productObject);
        }

        // POST: cms/ProductObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductObject productObject = db.ProductObjects.Find(id);
            db.ProductObjects.Remove(productObject);
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
