using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        IMSEntities5 db = new IMSEntities5();
        public ActionResult Index()
        {
            var sales = db.Sales.Include(p => p.ProductSales);
                return View(sales.ToList());
        }
        public ActionResult POS()
        {
            var products = db.Products;
            return View(products.ToList());
        }

        public ActionResult GetProductInReceipt(string qty,int productId)
             
        {
           
           
            ProductSale productSale = db.ProductSales.FirstOrDefault(a => a.ProductId == productId);
            Product product = db.Products.FirstOrDefault(a => a.ProductId == productId);

           
            ViewBag.InvoiceNumber = (db.Sales.Max(a => a.InvoiceNumber)) + 1;
            ViewBag.qty =Convert.ToInt32(qty);
            
            Sale itemTotal = new Sale();

            List<ProductSale> ProductSales = (List<ProductSale>)Session["ProductSale"];
            if (ProductSales == null)
            {
                ProductSales = new List<ProductSale>();
            }

             
            if (ProductSales.Exists(a => a.ProductId==productId))
            {
                
            }

            ProductSales.Add
            (
                new ProductSale()
                {
                    ProductId = product.ProductId,
                    SaleId = (db.Sales.Max(a => a.SaleId))+1,
                    SalePrice = product.SellingPrice,
                    SaleQuantity = ViewBag.qty,
                    TotalPrice = ViewBag.qty * product.SellingPrice,
                    Product = db.Products.FirstOrDefault(a => a.ProductId == product.ProductId)
                }
                );
            

            Session["ProductSale"] = ProductSales;

            itemTotal.ItemTotal = ProductSales.Sum(a => a.TotalPrice);
            ViewBag.ItemTotal = itemTotal.ItemTotal;

            decimal vat = System.Convert.ToDecimal(0.04);
            itemTotal.Vat = itemTotal.ItemTotal * vat;
            ViewBag.Vat = itemTotal.Vat;
            
            itemTotal.TotalAmoun = System.Convert.ToDecimal(itemTotal.ItemTotal + itemTotal.Vat);
            ViewBag.TotalAmount = itemTotal.TotalAmoun;


            List<Sale> sales = (List<Sale>)Session["Sale"];
            if (sales == null)
            {
                sales = new List<Sale>();
            }

            sales.Add(
                new Sale
                {
                    InvoiceNumber = ViewBag.InvoiceNumber,
                    SaleDateTime = DateTime.Now,
                    ItemTotal = ViewBag.ItemTotal,
                    Vat = ViewBag.Vat,
                    TotalAmoun = ViewBag.TotalAmount

                });

            Session["Sale"] = sales;

            //Find the total quantity
            var total = db.Products.Where(m => m.ProductCode == "B").Sum(a => a.ProductQuantity);

           var list= db.Products
        .GroupBy(m => m.ProductCode)
        .Select(g => g.Sum(c => c.ProductQuantity) )
        .ToList();

            POS();
            
            return View("POS");
            
        }


        public ActionResult Sale()
        {
            
            List<Sale> sales = new List<Sale>();
            sales = (List<Sale>)Session["Sale"];

            Sale sale = new Sale();
            foreach (var item in sales)
            {
                db.Sales.Add(
                    new Sale()
                    {
                        ProductId = 2,
                        SaleDateTime = item.SaleDateTime,
                        ItemTotal = item.ItemTotal,
                        Vat = item.Vat,
                        TotalAmoun = item.TotalAmoun,
                        TransactionId = 3,
                        InvoiceNumber = item.InvoiceNumber

                    }
                    );

                db.SaveChanges();
                
                
                
            }


            List<ProductSale> productSales = new List<ProductSale>();
            productSales =(List<ProductSale>)Session["ProductSale"];

            foreach(var item in productSales)
            {
                db.ProductSales.Add(
                    new ProductSale
                    {
                        ProductId = item.ProductId,
                        SaleId = item.SaleId,
                        SaleQuantity =item.SaleQuantity,
                        SalePrice = item.SalePrice,
                        TotalPrice =item.TotalPrice

                    });
                db.SaveChanges();


                db.Database.ExecuteSqlCommand("UPDATE [dbo].[Product] SET ProductQuantity = ProductQuantity-1 WHERE ProductId = '" + item.ProductId + "'");
            }
            
            Session.Remove("Sale");
            Session.Remove("ProductSale");
            POS();
            return View("POS");
        }


        public ActionResult DeleteFromReceipt(int id)
        {

            ProductSale productSale = db.ProductSales.FirstOrDefault(a => a.ProductId == id);
            List<ProductSale> removeproduct =new List < ProductSale >();
            removeproduct = (List<ProductSale>)Session["ProductSale"];

            var removableId = removeproduct.Find(a => a.ProductId == id);

            removeproduct.Remove(removableId);
            Session["ProductSale"] = removeproduct;

            POS();
            return View("POS");
        }

        public ActionResult SaleHold()
        {
            List<Sale> sales = new List<Sale>();
            sales = (List<Sale>)Session["OrderdProductList"];
            List<Transaction> transactions = new List<Transaction>();

            foreach (var item in sales)
            {
                db.SaleHolds.Add(
                    new SaleHold()
                    {
                        ProductId = item.ProductId,
                        //SaleHoldName = item.SaleProductName,
                        //SaleHoldPrice = item.SalePrice,
                        //SaleHoldDateTime = DateTime.Now,
                        //SaleHoldQuantity = item.SaleQuantity,
                        //SaleHoldTotalPrice = item.TotalPrice,

                        SaleId = 8,
                        TransactionId = 3

                    }
                    );
                //db.Transactions.Add(
                //new Transaction()
                //{
                //    InvoiceNumber = ViewBag.InvoiceNumber,
                //    ItemTotal = ViewBag.ItemTotal,
                //    Vat = ViewBag.Vat

                //}
                //);
                db.SaveChanges();
            }

            Session.Remove("OrderdProductList");
            POS();
            return View("POS");
        }


        public ActionResult ViewSaleHold()
        {
            
            return View(db.SaleHolds);
        }

        public ActionResult Unhold()
        {
            //GetProductInReceipt(2);
            return View("POS");
        }

    }


}