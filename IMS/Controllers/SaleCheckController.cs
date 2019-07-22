using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class SaleCheckController : Controller
    {
        IMSEntities5 db = new IMSEntities5();
        // GET: SaleCheck
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DateCheck()
        {
            return View();
        }

        public ActionResult Data()
        {
            IMSEntities5 db = new IMSEntities5();

            List<Product> list = new List<Product>();

            DateTime StartDate = Convert.ToDateTime("2019-07-11");
            DateTime EndDate = Convert.ToDateTime("2019-07-14");

            list.Where(x => x.ProductDate >= StartDate && x.ProductDate <= EndDate);

            return View();
        }

    }



}