using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class CategoryController : Controller
    {
        IMSEntities5 db = new IMSEntities5();
        // GET: Category
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AddNewCategory(string qty)
        {
            Category category = new Category();
                category.CategoryName = qty;
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                
                    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", category.CategoryId);
                    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", category.CategoryId);
                    return View(category);
            
        }
    }
}