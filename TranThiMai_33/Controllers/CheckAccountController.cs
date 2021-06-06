using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TranThiMai_33.Models;

namespace TranThiMai_33.Controllers
{
    public class CheckAccountController : Controller
    {
        public string CheckUserName { get; private set; }
        public string CheckPassword { get; private set; }

        Encrytion encry = new Encrytion();
        LTQLDb db = new LTQLDb();
        // GET: CheckAccount
        public ViewResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string CheckUsername, string CheckPassword)
        {
            if (string.IsNullOrEmpty(CheckUsername))
            {
                ViewBag.CheckUsernameError = " Nhập Username đi";
            }
            else if (string.IsNullOrEmpty(CheckPassword))
            {
                ViewBag.CheckPasswordError = " Nhập Password đi";
            }
            else
            {
                if (CheckUsername.Equals("Admin") && CheckPassword.Equals("123456"))
                {
                    FormsAuthentication.SetAuthCookie(CheckUsername, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.invalidData = "Nhập username = CheckUsername và pass = 123456 đi";
                }
            }
            ViewBag.CheckUsername = CheckUsername;
            return View();
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}