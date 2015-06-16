using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using CIR_skladisce.Filters;
using CIR_skladisce.Models;
using DbOperations;
using System.Data;

namespace CIR_skladisce.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UporabnikLogin model, string returnUrl)
        {
            DeloSPodatki db = new DeloSPodatki();
            DataTable tabelaUporabnik = db.Avtentifikacija(model.UporabniskoIme, model.Geslo);

            if (tabelaUporabnik.Rows.Count == 1)
            {
                Session["UserId"] = tabelaUporabnik.Rows[0]["id"].ToString();
                Session["UserName"] = tabelaUporabnik.Rows[0]["uporabnisko_ime"].ToString();
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UporabnikRegistartion model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    DeloSPodatki db = new DeloSPodatki();
                    db.Registracija(model);

                    return RedirectToAction("Login", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage()
        {
            DeloSPodatki db = new DeloSPodatki();
            Uporabnik uporabnik = db.getUporabnikaID(Convert.ToInt32(Session["UserId"]));

            if (uporabnik == null)
            {
                return HttpNotFound();
            }
            
            return View(uporabnik);
        }

        //
        // GET: /Account/EditUser

        public ActionResult EditUser()
        {
            DeloSPodatki db = new DeloSPodatki();
            Uporabnik uporabnik = db.getUporabnikaID(Convert.ToInt32(Session["UserId"]));

            if (uporabnik == null)
            {
                return HttpNotFound();
            }

            return View(uporabnik);
        }

        //
        // POST: /Account/EditUser

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Uporabnik uporabnik)
        {
            if (ModelState.IsValid)
            {
                DeloSPodatki db = new DeloSPodatki();
                db.updateUporabnik(uporabnik);
                return RedirectToAction("Manage");
            }

            return View(uporabnik);
        }

        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UporabnikChangePassword newPass)
        {
            if (ModelState.IsValid)
            {
                DeloSPodatki db = new DeloSPodatki();
                db.spremeniGeslo(Convert.ToInt32(Session["UserId"]), newPass.Geslo);

                return RedirectToAction("Manage");   
            }
            else
            {
                ModelState.AddModelError("", new ArgumentException("Napaka pri spremembi gesla"));
                return View();
            }
        }

        #region Helpers
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
