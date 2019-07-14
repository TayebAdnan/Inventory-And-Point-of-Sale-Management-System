using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class CeckController : Controller
    {
        IMSEntities3 db = new IMSEntities3();
        // GET: Ceck
        public ActionResult Index()

        {
            List<ProductSale> products = new List<ProductSale>();
            Session["OrderdProductList"] = products;
            return View();
        }
        [HttpGet]
        public ActionResult POS()
        {
           // Session.Remove("OrderdProductList");
            var products = db.Products;
            return View(products.ToList());
        }




        
        public ActionResult POS1(int id, FormCollection form)
        {
            
            int qty = Convert.ToInt32(form["test"]);

            Product product = db.Products.FirstOrDefault(a => a.ProductId == id);

            List<Product> Products = (List<Product>)Session["OrderdProductList"];
            if (Products == null) { Products = new List<Product>(); }
            Products.Add
            (
                new Product()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    SellingPrice = product.SellingPrice,
                    ProductQuantity = 2


                }
            );
            Session["OrderdProductList"] = Products;


            POS();
            
            //Session["ProductName"] =product.ProductName;
            return View("POS");
        }

        public ActionResult button()
        {
            return View();
        }
        
        public ActionResult GetSellingProduct(string id)
        {
            Session["ProductCode"] = id;
            return View();
        }
        [HttpPost]
        public string buttonClick(string userName, string password)
        {
            string securedInfo = "";
            if ((userName == "admin") && (password == "admin"))
                securedInfo = "Hello admin, your secured information is .......";
            else
                securedInfo = "Wrong username or password, please retry.";
            return securedInfo;
        }
        public ActionResult HttpPOS()
        {

            return View(db.Products.ToList());
        }
    }
}