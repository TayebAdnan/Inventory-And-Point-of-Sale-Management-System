using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class ProductsController : Controller
    {
        private IMSEntities5 db = new IMSEntities5();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Color).Include(p => p.Manufacture).Include(p => p.Size);
            
            return View(products.ToList());
            
            
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "ManufactureId", "LotNumber");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductCode,ProductName,CategoryId,ColorId,SizeId,ProductQuantity,AlertQuantity,SellingPrice,Image,ManufactureId,ProductDate")]
        Product product, HttpPostedFileBase ProductImage)
        {

            ManufactureProduct manufactureProduct = new ManufactureProduct();

            
            if (ModelState.IsValid)
            {

                try
                {
                    if (db.Products.Any(a => a.ProductCode == product.ProductCode))
                            {
                                db.Database.ExecuteSqlCommand("UPDATE [dbo].[Product] SET ProductQuantity = ProductQuantity+'"+product.ProductQuantity+"' WHERE ProductCode = '" + product.ProductCode + "'");
                            }

                    else { 
                        if (ProductImage != null)
                    {
                        // extract only the filename
                        var fileName = Path.GetFileNameWithoutExtension(ProductImage.FileName);
                        string extention = Path.GetExtension(ProductImage.FileName);
                        fileName = fileName + extention;
                        // store the file inside ~/App_Data/uploads folder
                        var path = Path.Combine(Server.MapPath("~/App_File/ProductImages/"), fileName);
                        var pathforDbSave = "/App_File/ProductImages/" + fileName;
                        product.ProductImage = pathforDbSave;
                        ProductImage.SaveAs(path);
                        db.Products.Add(product);
                        db.SaveChanges();

                    }
                        


                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

                int id = db.Products.Max(a => a.ProductId);

                db.ManufactureProducts.Add(
                    new ManufactureProduct
                    {
                        Quantity =Convert.ToInt32 (product.ProductQuantity),
                        ManufactureDateTime = Convert.ToDateTime (product.ProductDate),
                        ProductId =  id

                    }
                    );
                db.SaveChanges();


                 return RedirectToAction("Index");
                    
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
                ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName", product.ColorId);
                ViewBag.ManufactureId = new SelectList(db.Manufactures, "ManufactureId", "LotNumber", product.ManufactureId);
                ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", product.SizeId);
                return View(product);
            }
        



        // GET: Products/Edit/5
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName", product.ColorId);
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "ManufactureId", "LotNumber", product.ManufactureId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", product.SizeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductCode,ProductName,CategoryId,ColorId,SizeId,ProductQuantity,AlertQuantity,SellingPrice,Image,ManufactureId,ProductDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName", product.ColorId);
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "ManufactureId", "LotNumber", product.ManufactureId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName", product.SizeId);
            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult GetSearchValue(string search)
        {
            IMSEntities5 db = new IMSEntities5();
            var allsearch = (from c in db.Products
                             where c.ProductName.StartsWith(search)
                             select new { c.ProductName, c.ProductId });
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public JsonResult GetProducts(string Prefix)
        {

            var Products = (from c in db.Products
                             where c.ProductName.StartsWith(Prefix)
                             select new { c.ProductName, c.ProductId });
            return Json(Products, JsonRequestBehavior.AllowGet);
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
