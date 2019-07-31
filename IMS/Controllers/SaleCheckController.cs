using IMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class SaleCheckController : Controller
    {
        IMSEntities5 db = new IMSEntities5();
        // GET: SaleCheck
        
       
        public ActionResult Index(DateTime? start, DateTime? end)
        {
            
            var data=db.Sales.SqlQuery("select * from [dbo].[Sale] where SaleDateTime between '" + start + "'and'" + end + "'").ToList();
            
            return View(data);
        }
        public ActionResult DateCheck()
        {

            var count = db.Products.Where(p => p.ProductDate == DateTime.Today).Sum(p => p.ProductQuantity);
            ViewBag.TodaysProduct = count;
            return View();
        }

    }



}