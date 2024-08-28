using Inventory.AppContext;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class CustomerController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Customer
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
            ViewBag.Menu = "Customer";
            var model = db.costumers.Where(s => s.deletedBy == null).ToList();
            return View(model);
        }

        // GET: Customer/Details/5
        public ActionResult Detail(int id)
        {
            var model = db.costumers.Find(id);
            return PartialView("Detail",model);
        }

        // GET: Customer/Create
        public ActionResult CreateModal()
        {
            var model = new costumer();
            return PartialView("CreateModal", model);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(costumer model )
        {
            try
            {
                model.createdBy = Session["Username"].ToString();
                model.createdDate = DateTime.Now;
                db.costumers.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult EditModal(int id)
        {
            var model = db.costumers.Find(id);
            return PartialView("EditModal",model);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, costumer model)
        {
            try
            {
                var data = db.costumers.Find(id);
                data.namaCustomer = model.namaCustomer;
                data.alamatCustomer = model.alamatCustomer;
                data.contactCustomer = model.contactCustomer;
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var data = db.costumers.Find(id);
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
