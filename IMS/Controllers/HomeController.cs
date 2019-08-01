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

            if (sh_b_xl_001 >= alert)
            {
                ViewBag.alert = "Xl size Blue Shirt is in alerming rate";
                Inventorycount++;
            }

            ViewBag.countInventory = Inventorycount;

            return View();
        }

        public ActionResult Index()
        {
            Session.Remove("ProductSale");
            Session.Remove("Sale");

            var count = db.Products.Where(p => p.ProductDate == DateTime.Today).Sum(p => p.ProductQuantity);
            ViewBag.TodaysProduct = count;

            InventoryAlert();

            BarChart();

            return View();
        }

        public ActionResult BarChart()
        {
            try { 
            Session["Shirt"] = db.ProductSales.Where(a => (a.Product.ProductName == "Shirt") && (a.Sale.SaleDateTime == DateTime.Today)).Sum(a => a.SaleQuantity);
            }
            catch
            {
                Session["Shirt"] = 0;
            }
            return View();
        }
       

    }
}