using mvcotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace mvcotel.Controllers
{
    [Authorize(Roles ="Admin")]
    public class YonetimController : Controller
    {
        // GET: Yonetim
        [AllowAnonymous] // authorize da herşey kitlendi burda da sadece bunu açtık.
        public ActionResult Index()
        {
            return View();
        }
        //formu goster
        [HttpGet]
        public ActionResult SliderEkle()
        {
    
            return View();
        }
        //kayıt işlemi
        [HttpPost]
        public ActionResult SliderEkle(SliderModel s, HttpPostedFileBase resim)
        {
            if (resim != null && resim.ContentLength > 0)
            {
                s.ResimYolu = resim.FileName;
                string yol = Server.MapPath("/Content/slider/");
                yol += resim.FileName;
                if (System.IO.File.Exists(yol))// dosyaadı varsa 
                    yol.Replace(resim.FileName, Guid.NewGuid().ToString() + ".jpg");
                resim.SaveAs(yol);
            }
            if(ModelState.IsValid)
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.Sliderlar.Add(s);
                ctx.SaveChanges();
            }
            return View();
        }
        public ActionResult SliderListe()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Sliderlar.ToList());
        }
        public ActionResult SliderSil(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var silinecek = ctx.Sliderlar.Find(id);
            ctx.Sliderlar.Remove(silinecek);
            ctx.SaveChanges();
            return RedirectToAction("SliderListe");
        }
        [HttpGet]
        public ActionResult OdaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OdaEkle(OdaM oda,HttpPostedFileBase resim)
        {
            if(resim != null && resim.ContentLength>0)
            {
                oda.ResimURL = resim.FileName;
                string yol = Server.MapPath("/Content/oda/");
                resim.SaveAs(yol + resim.FileName);
            }
            if (ModelState.IsValid)
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.Odalar.Add(oda);
                ctx.SaveChanges();
            }
            return View();

        }


        [HttpGet]
        public ActionResult GaleriEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GaleriEkle(Galeri g,HttpPostedFileBase resim)
        {
            if (resim != null && resim.ContentLength > 0)
            {
                g.ResimYolu = resim.FileName;
                string yol = Server.MapPath("/Content/galeri/");
                resim.SaveAs(yol + resim.FileName);
            }
            if (ModelState.IsValid)
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.Galeriler.Add(g);
                ctx.SaveChanges();
            }
            return View();
        }

        public ActionResult GaleriSil(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var silinecek = ctx.Galeriler.Find(id);
            ctx.Galeriler.Remove(silinecek);
            ctx.SaveChanges();
            return RedirectToAction("GaleriListe");
        }
        public ActionResult GaleriListe()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Galeriler.ToList());
        }





        public ActionResult OdaListele()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            return View(ctx.Odalar.ToList());
        }
        [HttpGet]
        public ActionResult OdaDuzenle(int? id)
        {
            if (id == null)
            {
                ViewBag.mesaj = "bir oda şeçmediniz, id bekleniyor";
                return View();

            }else
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                var oda = ctx.Odalar.Find(id);
                return View();
            }
        }
        [HttpPost]
        public ActionResult OdaDuzenle(OdaM oda, HttpPostedFileBase resim)
        {
            var eski = "";
            using (ApplicationDbContext ctx2 = new ApplicationDbContext())
            {
                eski = ctx2.Odalar.Find(oda.OdaMID).ResimURL;
            }


            ApplicationDbContext ctx = new ApplicationDbContext();
           
            var klasor = Server.MapPath("/Content/oda");
            if(resim !=null && resim.ContentLength>0)
            {
                //eski resim silinmeli
                if (string.IsNullOrEmpty(eski))
                    System.IO.File.Delete(klasor+eski);
                //kayıt edilmeli
                resim.SaveAs(klasor + resim.FileName);
                //modeldeki url değişmeli
                oda.ResimURL = resim.FileName;         
            }
            else
            {
                //resim yüklenmemişse, eski resmi kaybetmemeiliyiz
                oda.ResimURL = eski;
            }
            if (ModelState.IsValid)
            {
                ctx.Entry(oda).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("OdaListele");
            }

            return View(oda);
        }
        public ActionResult OdaSil(int id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var silinecek = ctx.Odalar.Find(id);
            ctx.Odalar.Remove(silinecek);
            ctx.SaveChanges();
            return RedirectToAction("OdaListele");
        }

       
    }
}