using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaNarty.Models;
using System.IO;


namespace WypozyczalniaNarty.Controllers
{
    public class AdminController : Controller
    {
        KomisZad1Entities1 db = new KomisZad1Entities1();
        // GET: Admin
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Admin avm)
        {
            Admin ad = db.Admins.Where(x => x.AdminLogin == avm.AdminLogin && x.AdminPassword == avm.AdminPassword).SingleOrDefault();
            if (ad != null)
            {

                Session["ad_id"] = ad.AdminId.ToString();
                return RedirectToAction("Create");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }
        public ActionResult Create()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Narty cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Nie udalo sie wgrac obrazka";
            }
            else
            {
                Narty nar = new Narty();
                nar.NartaProducent = cvm.NartaProducent;
                nar.NartaModel = cvm.NartaModel;
                nar.NartaCena = cvm.NartaCena;
                nar.NartaZdjecie = path;
                nar.NartaStatus = true;
                db.Narties.Add(nar);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }

        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "Plik wgrany prawidlowo";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Obslugujace formaty zdjec to jpg ,jpeg or png'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Wybierz plik'); </script>");
                path = "-1";
            }



            return path;
        }
    }
}