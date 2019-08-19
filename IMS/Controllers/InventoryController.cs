using IMS.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace IMS.Controllers
{
    public class InventoryController : Controller
    {
        IMSEntities5 db = new IMSEntities5();
        // GET: Inventory

            public ActionResult Inventory()
        {
           
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ColorId = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "ManufactureId", "LotNumber");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeName");
            return View();
            
        }

        
        public ActionResult InventoryView(int CategoryId, int ColorId, int SizeId)
        {
            
            var count = db.Products.Where(p => (p.CategoryId == CategoryId) && (p.ColorId == ColorId) && (p.SizeId==SizeId)).Sum(p => p.ProductQuantity);
           
            ViewBag.InventoryCount = count;
            if(ViewBag.InventoryCount == null) {
                ViewBag.InventoryViewError = "This product is not available in Stock";

                Inventory();
                return View("Inventory");

            }
            else { 
            
                ViewBag.ProductName = db.Products.Find(CategoryId).ProductName;
                ViewBag.Color = db.Colors.Find(ColorId).ColorName;
                ViewBag.Size = db.Sizes.Find(SizeId).SizeName;
                return View("InventoryView");
            }


        }


        public ActionResult InventoryList()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Color).Include(p => p.Manufacture).Include(p => p.Size);

            return View(products.ToList());
        }
       
    }
}