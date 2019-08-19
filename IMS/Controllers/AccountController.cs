    using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace IMS.Controllers
{
    public class AccountController : Controller
    {
        IMSEntities5 db = new IMSEntities5();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objUser)
        {
            
                using (IMSEntities5 db = new IMSEntities5())
                {
                    var obj = db.Users.Where(a => a.UserEmail.Equals(objUser.UserEmail) && a.UserPassword.Equals(objUser.UserPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        Session["UserEmail"] = obj.UserEmail.ToString();
                        Session["UserPhone"] = obj.UserPhone.ToString();
                        Session["UserAddress"] = obj.UserAddress.ToString();
                        Session["RoleName"] = obj.Role.RoleName.ToString();
                    
                    if(obj.Role.RoleName == "Admin") {
                        return RedirectToAction("Index", "Home");
                    }

                    else if(obj.Role.RoleName == "Employee")
                    {
                        return RedirectToAction("POS","Sale");
                    }
                        
                    }

                else { 
                ViewBag.Error = "Username or Password is invalid";
                }
            }
            
            return View(objUser);
        }
       

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult MyProfile()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult RedirectToDefault()
        {

            String[] roles = Roles.GetRolesForUser();
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (roles.Contains("Employee"))
            {
                return RedirectToAction("POS", "Sale");
            }
            else if (roles.Contains("PublicUser"))
            {
                return RedirectToAction("Index", "PublicUser");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }


}