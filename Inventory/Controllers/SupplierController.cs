using Inventory.AppContext;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class SupplierController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Supplier
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string username = Session["Username"].ToString();
            string role = Session["Role"].ToString();
            ViewBag.username = username;
            ViewBag.role = role;
            ViewBag.Menu = "Supplier";
            var model = db.suppliers.Where(s=> s.deletedBy == null).ToList();
            return View(model);
        }

        // GET: Supplier/Details/5
        public ActionResult Detail(int id)
        {
            var model = db.suppliers.Find(id);
            return PartialView("Detail", model);
        }

        // GET: Supplier/Create
        public ActionResult CreateModal()
        {
            var model = new supplier();
            return PartialView("CreateModal", model);
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(supplier model)
        {
            try
            {
                model.createdDate = DateTime.Now;
                model.createdBy = Session["Username"].ToString();
                db.suppliers.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult EditModal(int id)
        {
            var model = db.suppliers.Find(id);
            return PartialView("EditModal", model);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, supplier model)
        {
            try
            {
                
                var data = db.suppliers.Find(id);
                data.namaSupplier = model.namaSupplier;
                data.alamatSupplier = model.alamatSupplier;
                data.contactSuplier = model.contactSuplier;
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

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var data = db.suppliers.Find(id);
                data.deletedBy = Session["Username"].ToString();
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
