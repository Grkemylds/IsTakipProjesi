using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvsFirmaCagri1.Models.Entity;

namespace MvsFirmaCagri1.Controllers
{
    [Authorize]

    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        DbisTakipEntities db = new DbisTakipEntities();

        public ActionResult AktifCagrılar()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            ViewBag.m = mail;
            var cagrılar = db.TblCagrilar.Where(x => x.Durum == true && x.CagriFirma == id).ToList();
            return View(cagrılar);
        }
        public ActionResult PasifCagrılar()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrılar = db.TblCagrilar.Where(x => x.Durum == false && x.CagriFirma == id).ToList();
            return View(cagrılar);
        }
        [HttpGet]
        public ActionResult YeniCagri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCagri(TblCagrilar p)
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            p.Durum = true;
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CagriFirma = id;
            db.TblCagrilar.Add(p);
            db.SaveChanges();
            return RedirectToAction("AktifCagrılar");
        }
        public ActionResult CagriDetay(int id)
        {
            var cagri = db.TblCagrıDetay.Where(x => x.Çağrı == id).ToList();
            return View(cagri);
        }
        public ActionResult CagriGetir(int id)
        {
            var cagri = db.TblCagrilar.Find(id);
            return View("CagriGetir", cagri);
        }
        public ActionResult CagriDüzenle(TblCagrilar p)
        {
            var cagri = db.TblCagrilar.Find(p.ID);
            cagri.Konu = p.Konu;
            cagri.Açıklama = p.Açıklama;
            db.SaveChanges();
            return RedirectToAction("AktifCagrılar");
        }
        [HttpGet]
        public ActionResult ProfilDüzenle()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            var profil = db.TblFirmalar.Where(x => x.ID == id).FirstOrDefault();
            return View(profil);
        }
        public ActionResult AnaSayfa()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var ToplamCagri = db.TblCagrilar.Where(x => x.CagriFirma == id).Count();
            var AktifCagrılar = db.TblCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            var PasifCagrılar = db.TblCagrilar.Where(x => x.CagriFirma == id && x.Durum == false).Count();
            var yetkili = db.TblFirmalar.Where(x => x.ID == id).Select(y => y.Yetkili).FirstOrDefault();
            var seckor = db.TblFirmalar.Where(x => x.ID == id).Select(y => y.Sektör).FirstOrDefault();
            var firmaadi = db.TblFirmalar.Where(x => x.ID == id).Select(y => y.Ad).FirstOrDefault();
            var gorsel = db.TblFirmalar.Where(x => x.ID == id).Select(y => y.Görsel).FirstOrDefault();

            ViewBag.c1 = ToplamCagri;
            ViewBag.c2 = AktifCagrılar;
            ViewBag.c3 = PasifCagrılar;
            ViewBag.c4 = yetkili;
            ViewBag.c5 = seckor;
            ViewBag.c6 = firmaadi;
            ViewBag.c7 = gorsel;

            return View();
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var mesajlar = db.TblMesajlar.Where(x => x.Alıcı == id && x.Durum == true).ToList();
            var mesajsayisi = db.TblMesajlar.Where(x => x.Alıcı == id && x.Durum == true).Count();
            ViewBag.m1 = mesajsayisi;
            return PartialView(mesajlar);
        }
        public PartialViewResult Partial2()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrılar = db.TblCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).ToList();
            var cagrisayisi = db.TblCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            ViewBag.m1 = cagrisayisi;
            return PartialView(cagrılar);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial3()
        {
            return PartialView();
        }

    }
}