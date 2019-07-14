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
        IMSEntities3 db = new IMSEntities3();

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
            if (ModelState.IsValid)
            {
                using (IMSEntities3 db = new IMSEntities3())
                {
                    var obj = db.Users.Where(a => a.UserEmail.Equals(objUser.UserEmail) && a.UserPassword.Equals(objUser.UserPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        Session["UserEmail"] = obj.UserEmail.ToString();
                        Session["UserPhone"] = obj.UserPhone.ToString();
                        Session["UserAddress"] = obj.UserAddress.ToString();
                        return RedirectToAction("MyProfile");
                    }
                }
            }
            return View(objUser);
        }
       

        //public ActionResult Login(User model, string returnUrl)
        //{
        //    IMSEntities3 db = new IMSEntities3();
        //    var dataItem = db.Users.Where(x => x.UserEmail == model.UserEmail && x.UserPassword == model.UserPassword).First();
        //    if (dataItem != null)
        //    {
        //        FormsAuthentication.SetAuthCookie(dataItem.UserName, false);
        //        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                 && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("RedirectToDefault");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid user/pass");
        //        return View();
        //    }
        //}


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
                return RedirectToAction("Index", "Products");
            }
            else if (roles.Contains("Dealer"))
            {
                return RedirectToAction("Index", "Dealer");
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