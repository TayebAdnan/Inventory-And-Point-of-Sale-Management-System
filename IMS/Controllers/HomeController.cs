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
            int Inventorycount = 0;

            var sh_b_xl_001 = db.Products.Where(p => p.ProductCode == "sh-b-xl-001").Sum(p => p.ProductQuantity);
            var sh_b_l_001 = db.Products.Where(p => p.ProductCode == "sh-b-l-001").Sum(p => p.ProductQuantity);
            var sh_b_m_001 = db.Products.Where(p => p.ProductCode == "sh-b-m-001").Sum(p => p.ProductQuantity);
            var sh_b_s_001 = db.Products.Where(p => p.ProductCode == "sh-b-m-001").Sum(p => p.ProductQuantity);
            var alert = db.Products.FirstOrDefault(p => p.ProductCode == "sh-b-xl-001").AlertQuantity;


            if (sh_b_xl_001 <= alert)
            {
                ViewBag.alert = "Xl size Blue Shirt is in alerming rate";
                Inventorycount++;
            }

            var allCategory = db.Categories.Select(a => a.CategoryId).ToList();

            var sh = db.Products.Where(p => p.ProductCode == "sh-b-xl-001").Sum(p => p.ProductQuantity);



            ViewBag.countInventory = Inventorycount;

            return View();
        }

        public ActionResult Index()
        {
            Session.Remove("ProductSale");
            Session.Remove("Sale");

            //Todays Added Product
            var count = db.ManufactureProducts.Where(p => p.ManufactureDateTime == DateTime.Today).Sum(p => p.Quantity);
            ViewBag.TodaysProduct = count;

            InventoryAlert();

            BarChart();
            
           //Sales in this month

            DateTime firstOfWeek = DateTime.Today; 
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