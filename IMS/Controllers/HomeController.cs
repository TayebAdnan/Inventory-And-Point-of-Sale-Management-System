using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        IMSEntities5 db = new IMSEntities5();


        public ActionResult InventoryAlert()
        {
            
            ViewBag.countInventory = db.Products.Where(x => (x.ProductQuantity <= x.AlertQuantity)).Count();
            
            return View();
        }

        public ActionResult Index()
        {
            Session.Remove("ProductSale");
            Session.Remove("Sale");

            //Todays Added Product
            try { 
            var count = db.ManufactureProducts.Where(p => p.ManufactureDateTime == DateTime.Today).Sum(p => p.Quantity);
            ViewBag.TodaysProduct = count;
            }
            catch { ViewBag.TodaysProduct = 0; }
            InventoryAlert();

            BarChart();
            
           //Sales in this month

            DateTime firstOfWeek = DateTime.Today.AddDays(1); 
            DateTime lastofWeek = firstOfWeek.AddDays(-7);
            var SaleReports =0;
            try { 
             SaleReports = db.Sales.Where(s => s.SaleDateTime <= firstOfWeek && s.SaleDateTime >= lastofWeek).Sum( s => s.ProductSales.Sum(z => z.SaleQuantity));

            }
            catch
            {
                 SaleReports = 0;
            }
            ViewBag.SaleReports = SaleReports;


            //Adjustments in this week
            try
            {
                ViewBag.Adjustments = db.Adjustments.Where(x => (x.AdjustedDateTime <= firstOfWeek && x.AdjustedDateTime >= lastofWeek)).Sum(a => a.AdjustedQuantity);
            }
            catch { ViewBag.Adjustments = 0; }
            return View();
        }

       

        public ActionResult BarChart()
        {
            var DayBeforeSeven = DateTime.Today.AddDays(-7);
            try { 
            Session["Shirt"] = db.ProductSales.Where(a => (a.Product.ProductName == "Shirt") && (a.Sale.SaleDateTime <= DateTime.Today && a.Sale.SaleDateTime >= DayBeforeSeven)).Sum(a => a.SaleQuantity);
            
            }
            catch
            {
                Session["Shirt"] = 0;
               
            }

            try
            {
                Session["JeansPant"] = db.ProductSales.Where(a => (a.Product.ProductName == "Jeans Pant") && (a.Sale.SaleDateTime <= DateTime.Today && a.Sale.SaleDateTime >= DayBeforeSeven)).Sum(a => a.SaleQuantity);
            }
            catch
            {
                Session["JeansPant"] = 0;

            }
            try
            {
                Session["GabadinPant"] = db.ProductSales.Where(a => (a.Product.ProductName == "Gabadin Pant") && (a.Sale.SaleDateTime <= DateTime.Today && a.Sale.SaleDateTime >= DayBeforeSeven)).Sum(a => a.SaleQuantity);
            }
            catch { Session["GabadinPant"] = 0; }
            try { Session["T-Shirt"] = db.ProductSales.Where(a => (a.Product.ProductName == "T-Shirt") && (a.Sale.SaleDateTime <= DateTime.Today && a.Sale.SaleDateTime >= DayBeforeSeven)).Sum(a => a.SaleQuantity); }
            catch { Session["T-Shirt"] = 0; }
            try { Session["Punjabi"] = db.ProductSales.Where(a => (a.Product.ProductName == "Punjabi") && (a.Sale.SaleDateTime <= DateTime.Today && a.Sale.SaleDateTime >= DayBeforeSeven)).Sum(a => a.SaleQuantity); }
            catch { Session["Punjabi"] = 0; }
            return View();
        }
       
        public ActionResult GroupYearMonth()
        {
            var model = db.Products.GroupBy(a => new
            {

            });
            return View();
        }
    }
}   