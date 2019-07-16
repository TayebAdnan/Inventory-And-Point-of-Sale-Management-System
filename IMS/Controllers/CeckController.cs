using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;
using static System.Net.WebRequestMethods;

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
        public ActionResult POS2(int id, FormCollection form)
        {

            int qty = Convert.ToInt32(form["test"]);

            Sale sale = db.Sales.FirstOrDefault(a => a.ProductId == id);
            Product product = db.Products.FirstOrDefault(a => a.ProductId == id);
            //db.Sales.OrderByDescending(p => p.InvoiceNumber).FirstOrDefault();
            var max = db.Sales.Max(p => p.SaleId);
            //ViewBag.InvoiceNumber = max; 

            List<Sale> Sales = (List<Sale>)Session["OrderdProductList"];
            if (Sales == null)
            {
                Sales = new List<Sale>();
            }

            Sales.Add
            (
                new Sale()
                {
                    ProductId =product.ProductId,
                    SaleDateTime = DateTime.Now,
                    
                    SaleProductName = product.ProductName,
                    SaleQuantity = 2,
                    SalePrice = product.SellingPrice,
                    TotalPrice = 2* (db.Products.FirstOrDefault(a=>a.ProductId==product.ProductId).SellingPrice),
                }
            );
            Session["OrderdProductList"] = Sales;
            POS();
            return View("POS");
        }
       
        

        public ActionResult Sale()
        {
            List<Sale> sales = new List<Sale>();
             sales = (List<Sale>)Session["OrderdProductList"];
            foreach(var item in sales)
            {
                db.Sales.Add(
                    new Sale()
                    {
                        ProductId = item.ProductId,
                        SaleProductName = item.SaleProductName,
                        SaleDateTime = item.SaleDateTime,
                        SalePrice = item.SalePrice,
                        SaleQuantity = item.SaleQuantity,
                        
                        TotalPrice = item.TotalPrice
                    }
                    );
                db.SaveChanges();
            }

            Session.Remove("OrderdProductList");
            POS();
            return View("POS");
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