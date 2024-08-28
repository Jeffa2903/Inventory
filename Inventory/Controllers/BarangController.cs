using Inventory.AppContext;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class BarangController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Barang
        public ActionResult Index()
        {
            if(Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string username = Session["Username"].ToString();
            string role = Session["Role"].ToString();
            ViewBag.username = username;
            ViewBag.role = role;
            ViewBag.Menu = "Barang";
            var barang = db.barangs.Where(s=> s.deletedBy == null && s.deletedDate == null).ToList();
            return View(barang);
        }

        // GET: Barang/Details/5
        public ActionResult Details(int id)
        {
            var model = db.barangs.Find(id);
            return PartialView("detailModal", model);
        }

        // GET: Barang/Create

        public ActionResult CreateModal()
        {
            var model = new barang();
            return PartialView("CreateModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(barang model, string jenis)
        {
            model.jenis = jenis;    
            model.createdDate = DateTime.Now;
            model.createdBy = Session["Username"].ToString();
            db.barangs.Add(model);
            db.SaveChanges();
            return Redirect("Index");
        }

        

        // GET: Barang/Edit/5
        public ActionResult EditModal(int id)
        {
            var model = db.barangs.Find(id);
            return PartialView("EditModal", model);
        }

        // POST: Barang/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, barang model)
        {
            try
            {
                var data = db.barangs.Find(id);

                data.namaBarang = model.namaBarang;
                data.satuan = model.satuan;
                data.hargaBarang = model.hargaBarang;
                data.updatedBy = Session["Username"].ToString();
                data.updatedDate = DateTime.Now;
                db.SaveChanges();   

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Barang/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Barang/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var data = db.barangs.Find(id);

                data.deletedBy = Session["Username"].ToString() ;
                data.deletedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
