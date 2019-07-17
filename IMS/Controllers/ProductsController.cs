using System;
using System.Collections.Generic;
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
        private IMSEntities4 db = new IMSEntities4();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Color).Include(p => p.Manufacture).Include(p => p.Size);
            
            return View(products.ToList());
            
            

            //return Json(new { data = products.ToList() }, JsonRequestBehavior.AllowGet);
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

        //POST: Products/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductCode,ProductName,CategoryId,ColorId,SizeId,ProductQuantity,AlertQuantity,SellingPrice,Image,ManufactureId,ProductDate")]
       Product imageModel, Product product)
        {
            //string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            //string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            //fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            //imageModel.ProductImage = "~/App_File/ProductImages/" + fileName;
            //fileName = Path.Combine(Server.MapPath("~/App_File/ProductImages/"), fileName);
            //imageModel.ImageFile.SaveAs(fileName);
            using (IMSEntities4 dba = new IMSEntities4())
            {
                dba.Products.Add(imageModel);
                dba.SaveChanges();

            }

            if (ModelState.IsValid)
            {
                        db.Products.Add(product);
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
            IMSEntities4 db = new IMSEntities4();
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
