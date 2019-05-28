using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    public class ProductImagesController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _productImageFolder = "/Uploads/images/product/item";
        // GET: cms/ProductImages
        public ActionResult Index(int productId)
        {
            ViewBag.ProductId = productId;
            var productImages = db.ProductImages.Where(x => x.ProductId == productId).Include(p => p.Product);
            return View(productImages.ToList());
        }

        // GET: cms/ProductImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: cms/ProductImages/Create
        public ActionResult Create(int productId)
        {
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductImage productImage, IEnumerable<HttpPostedFileBase> ImageFiles)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.FirstOrDefault(x => x.ProductId == productImage.ProductId);
                if (product == null)
                {
                    return ReturnJson();
                }
                var productUrl = product.ProductNameUrl;
                if (ImageFiles != null && ImageFiles.Count() > 0)
                {
                    foreach (var item in ImageFiles)
                    {
                        if (item != null && item.ContentLength > 0)
                        {
                            ProductImage itemimage = new ProductImage()
                            {
                                ProductId = productImage.ProductId,

                            };
                            string error = "";
                            string imageurl = "";
                            var isupload = UploadImage(ref error, ref imageurl, item, productUrl, _productImageFolder);
                            if (!isupload)
                                return ReturnJson(error);
                            itemimage.ImageUrl = imageurl;
                            db.ProductImages.Add(itemimage);
                            db.SaveChanges();
                        }
                    }
                }
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

        // GET: cms/ProductImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductId", "ProductName", productImage.ProductId);
            return View(productImage);
        }

        // POST: cms/ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductImageId,ImageUrl,IsActive,ProductID")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductId", "ProductName", productImage.ProductId);
            return View(productImage);
        }

        // GET: cms/ProductImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: cms/ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            db.ProductImages.Remove(productImage);
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
