using IMS.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class ReportController : Controller
    {
        IMSEntities5 db = new IMSEntities5();
        // GET: Report
        public ActionResult GetDateRange()
        {
            return View();
        }

        public ActionResult SaleReport(DateTime start, DateTime end)
        {

            List<Sale> list = new List<Sale>();

            list = db.Sales.Where(x => (x.SaleDateTime >= start && x.SaleDateTime <= end)).ToList();
           
            
            return View(list);
        }

        public ActionResult TodaysProduct()
        {

            var count = db.Products.Where(p => p.ProductDate == DateTime.Today).Sum(p => p.ProductQuantity);
            ViewBag.TodaysProduct = count;
            return View();
        }
    }
}