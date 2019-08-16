using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class RequisitionController : Controller
    {

        IMSEntities5 db = new IMSEntities5();
        // GET: Requisition
        public ActionResult Index(Product product)
        {

            Session.Remove("Requisition");
            //List<Product> repo = new List<Product>();

            List<Product> repo = (List<Product>)Session["Requisition"];
            if (repo == null)
            {
                repo = new List<Product>();
            }


            List<Product> products = new List<Product>();

            products = db.Products.Where(y => (y.ProductQuantity <= y.AlertQuantity)).ToList();

            Session["Requisition"] = products;
            
            
            


            return View();
        }
    }
}