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
using NamTanUyen.Filters;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]

    public class BrandsController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _productImageFolder = "/Uploads/images/product/brand";
        // GET: cms/Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: cms/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: cms/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Brand brand, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                brand.BrandUrl = CommonHelper.BuildUrl(brand.BrandName);
                brand.CreatedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, brand.BrandUrl, _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        brand.ImageUrl = imageurl;
                    }
                }
                db.Brands.Add(brand);
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
        // GET: cms/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Brand brand, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                brand.BrandUrl = CommonHelper.BuildUrl(brand.BrandName);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, brand.BrandUrl, _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        brand.ImageUrl = imageurl;
                    }
                }
                db.Entry(brand).State = EntityState.Modified;
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
        // GET: cms/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: cms/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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
