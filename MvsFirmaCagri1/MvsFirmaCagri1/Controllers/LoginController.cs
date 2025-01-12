using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvsFirmaCagri1.Models.Entity;

namespace MvsFirmaCagri1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbisTakipEntities db = new DbisTakipEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TblFirmalar p)
        {
            var bilgiler = db.TblFirmalar.FirstOrDefault(x => x.Mail == p.Mail && x.Şifre == p.Şifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["Mail"] = bilgiler.Mail.ToString();
                return RedirectToAction("AktifCagrılar", "Default");
            }
            else
            {
                return RedirectToAction("Index");

            }
        }
    }
}