using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcotel.Models;
using Microsoft.AspNet.Identity;

namespace mvcotel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Sliderlar.OrderBy(x => x.Sira).ToList());
        }
        public ActionResult Services()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult ActionSection()
        {
            return View();
        }
        public ActionResult OurTeam()
        {
            return View();
        }
        public ActionResult Testimonial()
        {
            return View();
        }
        public ActionResult Portfolio()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Galeriler.ToList());
        }
        public ActionResult Pricing() //odalar
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Odalar.OrderBy(x=>x.Fiyat).ToList());
        }
        public ActionResult Business()
        {
            return View();
        }
        public ActionResult Bottom()
        {
            return View();
        }

        [Authorize]
        public ActionResult Rezervasyon(int? id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            if (id == null)
                return RedirectToAction("OdaSec");
            var secilen = ctx.Odalar.Find(id);
            return View(secilen);
        }

        public string OdaSec()
        {
            return "Sayfaya id gelmediği için hangi odayı şeçtiğinizi bilmiyorum. Geri dönüp tekrar deneyin.";
        }



        [HttpGet]
        public ActionResult RezervasyonForm(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            RezervasyonM r = new RezervasyonM();
            r.OdaID = id;
            r.UserID = User.Identity.GetUserId();
            ViewBag.OdaFiyat = ctx.Odalar.Find(id).Fiyat;
            return View(r);
        }

        [HttpPost]
        public ActionResult RezervasyonForm(RezervasyonM r)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var odafiyat = ctx.Odalar.Find(r.OdaID).Fiyat;
            r.Fiyat = odafiyat * r.KacKisi;
            if (ModelState.IsValid)
            {
                ctx.Rezervasyonlar.Add(r);
                ctx.SaveChanges();
                    }
            return View();
        }

    }
}

