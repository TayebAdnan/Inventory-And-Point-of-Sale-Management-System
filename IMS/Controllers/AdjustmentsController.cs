using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class AdjustmentsController : Controller
    {
        private IMSEntities5 db = new IMSEntities5();

        // GET: Adjustments
        public ActionResult Index()
        {
            var adjustments = db.Adjustments.Include(a => a.Product);
            return View(adjustments.ToList());
        }

        // GET: Adjustments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = db.Adjustments.Find(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            return View(adjustment);
        }

        // GET: Adjustments/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductCode");
            return View();
        }

        // POST: Adjustments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdjustmentId,AdjustmentReason,ProductId,AdjustedQuantity,AdjustedDateTime")] Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                db.Adjustments.Add(adjustment);
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("UPDATE [dbo].[Product] SET ProductQuantity = ProductQuantity-1 WHERE ProductId = '" + adjustment.ProductId + "'");
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductCode", adjustment.ProductId);
            return View(adjustment);
        }

        // GET: Adjustments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = db.Adjustments.Find(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId",  "AdjustedDateTime", adjustment.ProductId);
            return View(adjustment);
        }

        // POST: Adjustments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdjustmentId,AdjustmentReason,ProductId,AdjustedQuantity,AdjustedDateTime")] Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adjustment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductCode", adjustment.ProductId);
            return View(adjustment);
        }

        // GET: Adjustments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = db.Adjustments.Find(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            return View(adjustment);
        }

        // POST: Adjustments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adjustment adjustment = db.Adjustments.Find(id);
            db.Adjustments.Remove(adjustment);
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
