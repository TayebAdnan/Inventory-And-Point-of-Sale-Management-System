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
        public ActionResult Index()
        {
            Session.Remove("OrderdProductList");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult JJA(string search)
        {
            return View();
        }
        public JsonResult GetProductByName(string search)
        {
            IMSEntities5 db = new IMSEntities5();
            //var allsearch = (from c in db.Products
            //                 where c.ProductName.StartsWith(search)
            //                 select new { c.ProductName, c.ProductId });
            var products = db.Products.Where(a => a.ProductName.Contains(search)).ToList();
            return new JsonResult { Data = products, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }

    }
}