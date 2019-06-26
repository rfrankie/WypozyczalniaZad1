using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WypozyczalniaNarty.Models;

namespace WypozyczalniaNarty.Controllers
{
    public class UserController : Controller
    {
       KomisZad1Entities1 db = new KomisZad1Entities1();
        // GET: User
        public ActionResult Index(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Narties.Where(x => x.NartaStatus == true).OrderByDescending(x => x.NartaId).ToList();
            IPagedList<Narty> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);
        }
        public ActionResult SignUp()
        {

            return View();
        }


        [HttpPost]
        public ActionResult SignUp(Klienci uvm)
        {
            Klienci u = new Klienci();
            u.KlientImie = uvm.KlientImie;
            u.KlientNazwisko = uvm.KlientNazwisko;
            u.KlientPesel = uvm.KlientPesel;
            u.KlientMail = uvm.KlientMail;
            u.KlientLogin = uvm.KlientMail;
            u.KlientPassword = uvm.KlientPassword;
            u.KlientTel = uvm.KlientTel;
            db.Kliencis.Add(u);
            db.SaveChanges();
            return RedirectToAction("login");

            return View();
        }

        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(Klienci avm)
        {
            Klienci ad = db.Kliencis.Where(x => x.KlientMail == avm.KlientMail && x.KlientPassword == avm.KlientPassword).SingleOrDefault();
            if (ad != null)
            {

                Session["u_id"] = ad.KlientId.ToString();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.error = "Niepoprawny mail lub haslo.";

            }

            return View();
        }

        [HttpGet]
        public ActionResult GetSkis()
        {
            List<Narty> liP = db.Narties.ToList();
            ViewBag.categorylist = new SelectList(liP, "NartaId", "NartaProducent");

            List<Narty> liM = db.Narties.ToList();
            ViewBag.categorylist = new SelectList(liM, "NartaId", "NartaModel");

            return View();
        }

        [HttpPost]
        public ActionResult GetSkis(Narty nvm, Klienci avm)
        {
            List<Narty> liP = db.Narties.ToList();
            ViewBag.categorylist = new SelectList(liP, "NartaId", "NartaProducent");

            List<Narty> liM = db.Narties.ToList();
            ViewBag.categorylist = new SelectList(liM, "NartaId", "NartaModel");

            Narty nId = db.Narties.Where(x => x.NartaProducent == nvm.NartaProducent && x.NartaModel == nvm.NartaModel).SingleOrDefault();
            if (nId != null)
            {
                Narty n = new Narty();
                Transakcje t = new Transakcje();
                t.NartaId = nId.NartaId;
                t.KlientId = Int32.Parse(Session["u_id"].ToString());
                p.pro_price = pvm.pro_price;
                p.pro_image = path;
                p.pro_fk_cat = pvm.pro_fk_cat;
                p.pro_des = pvm.pro_des;
                p.pro_fk_user = Convert.ToInt32(Session["u_id"].ToString());
                db.Transakcjes.Add(n);
                db.SaveChanges();
                Response.Redirect("index");

            }
            else
            {
                ViewBag.error = "Niepoprawne dane nart.";

            }

            
            return View();
        }

        

    }
}