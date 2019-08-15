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
            List<Product> repo = new List<Product>();

            repo = (List<Product>)Session["Requisition"];
            if (repo == null)
            {
                repo = new List<Product>();
            }
            var a = db.Products.ToArray();
            
            
            for ( int i = 0; i< a.Length; i++)
            {
                if(a[i].ProductQuantity <= a[i].AlertQuantity)
                {
                    repo.Add(
                        new Product
                        {
                            ProductId = a[i].ProductId,
                            ProductName = a[i].ProductName,
                            ProductQuantity = a[i].ProductQuantity + 10,
                            
                            ColorId = a[i].ColorId,
                            SizeId = a[i].SizeId
                            //Color = db.Colors.FirstOrDefault(b => b.ColorId == a[i].ColorId),
                            //Size = db.Sizes.FirstOrDefault(b => b.SizeId == a[i].SizeId)

                        }

                        );

                }
            }

            Session["Requisition"] = repo;
            
            
            


            return View();
        }
    }
}