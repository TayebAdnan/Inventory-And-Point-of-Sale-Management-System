using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        IMSEntities4 db = new IMSEntities4();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult POS()
        {
            var products = db.Products;
            return View(products.ToList());
        }

        public ActionResult GetProductInReceipt(int id, FormCollection form)
        {

            int qty = Convert.ToInt32(form["test"]);

            Sale sale = db.Sales.FirstOrDefault(a => a.ProductId == id);
            Product product = db.Products.FirstOrDefault(a => a.ProductId == id);

            ViewBag.InvoiceNumber = db.Transactions.Max(a => a.InvoiceNumber)+1;

            Transaction itemTotal = new Transaction();

            
            List<Sale> Sales = (List<Sale>)Session["OrderdProductList"];


            if (Sales == null)
            {
                Sales = new List<Sale>();
            }
            
            Sales.Add
            (
                new Sale()
                {
                    ProductId = product.ProductId,
                    SaleDateTime = DateTime.Now,

                    SaleProductName = product.ProductName,
                    SaleQuantity = 2,
                    SalePrice = product.SellingPrice,
                    TotalPrice = 2 * (db.Products.FirstOrDefault(a => a.ProductId == product.ProductId).SellingPrice),
                }
            );
            Session["OrderdProductList"] = Sales;


            itemTotal.ItemTotal = Sales.Sum(a => a.TotalPrice);
            ViewBag.ItemTotal = itemTotal.ItemTotal;


            decimal vat = System.Convert.ToDecimal(0.04);
            itemTotal.Vat = itemTotal.ItemTotal * vat;
            ViewBag.Vat = itemTotal.Vat;

            itemTotal.TotalAmount = System.Convert.ToDecimal(itemTotal.ItemTotal + itemTotal.Vat);
            ViewBag.TotalAmount = itemTotal.TotalAmount;

            
           
            POS();
            return View("POS");
        }


        public ActionResult Sale()
        {
            
            List<Sale> sales = new List<Sale>();
            sales = (List<Sale>)Session["OrderdProductList"];
            Transaction itemTotal = new Transaction();
            itemTotal.ItemTotal = 0;
            foreach (var item in sales)
            {
                db.Sales.Add(
                    new Sale()
                    {
                        ProductId = item.ProductId,
                        SaleProductName = item.SaleProductName,
                        SaleDateTime = item.SaleDateTime,
                        SalePrice = item.SalePrice,
                        SaleQuantity = item.SaleQuantity,
                        TotalPrice = item.TotalPrice,
                        TransactionId = 3
                    }
                    );
                db.Transactions.Add(
                new Transaction()
                {
                    InvoiceNumber = ViewBag.InvoiceNumber,
                    ItemTotal = ViewBag.ItemTotal
                   
                    
                }
                );
                db.SaveChanges();
            }

            

            Session.Remove("OrderdProductList");
            POS();
            return View("POS");
        }

    }


}