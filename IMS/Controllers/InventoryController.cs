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
            ViewBag.ProductName = db.Products.Find(CategoryId).ProductName;
            ViewBag.Color = db.Colors.Find(ColorId).ColorName;
            ViewBag.Size = db.Sizes.Find(SizeId).SizeName;
            return View();
        }


        public ActionResult InventoryReport()
        {
            return View();
        }
        public ActionResult InventoryAlert()
        {
            
            var sh_b_xl_001 = db.Products.Where(p => p.ProductCode == "sh-b-xl-001").Sum(p => p.ProductQuantity);
            var sh_b_l_001 = db.Products.Where(p => p.ProductCode == "sh-b-l-001").Sum(p => p.ProductQuantity);
            var sh_b_m_001 = db.Products.Where(p => p.ProductCode == "sh-b-m-001").Sum(p => p.ProductQuantity);
            var sh_b_s_001 = db.Products.Where(p => p.ProductCode == "sh-b-m-001").Sum(p => p.ProductQuantity);
            var alert = db.Products.FirstOrDefault(p => p.ProductCode == "sh-b-xl-001").AlertQuantity;
            
            if(sh_b_xl_001 >= alert)
            {
                ViewBag.alert = "Xl size Blue Shirt is in alerming rate";
                
            }
            return View();
        }
    }
}