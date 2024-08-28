using Inventory.AppContext;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class BarangMasukController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: BarangKeluar
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
            ViewBag.Menu = "Barang Masuk";
            var model = from s in db.barangMasuks 
                        join b in db.barangs
                        on s.idBarang equals b.id 
                        join ss in db.suppliers on s.idSupllierBarang equals ss.id 
                        where b.deletedDate == null && ss.deletedDate == null && s.deletedDate == null
                        select new BarangMasukView
                        
                        {
                            Id = s.ID,
                            TanggalMasuk = s.tanggalMasuk,
                            IdBarang = b.id,
                            NamaBarang = b.namaBarang,
                            JumlahBarang = s.jumlahBarang,
                            HargaBarang = b.hargaBarang,
                            HargaBarangTotal = b.hargaBarang * s.jumlahBarang,
                            Suplier = ss.namaSupplier,
                            satuan = b.satuan,
                            idSuplier = s.idSupllierBarang
                        };

            return View(model);
        }

        // GET: BarangKeluar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BarangKeluar/Create
        public ActionResult Create()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            string username = Session["Username"].ToString();
            string role = Session["Role"].ToString();
            ViewBag.username = username;
            ViewBag.role = role;
            ViewBag.Menu = "Input Data Barang Masuk";
            ViewBag.supplier = db.suppliers.Where(s => s.deletedDate == null && s.deletedBy == null).ToList();
            ViewBag.barang = db.barangs.Where(s=> s.deletedDate == null && s.deletedBy == null).ToList();
            return View();
        }

        // POST: BarangKeluar/Create
        [HttpPost]
        public ActionResult CreateSave(List<barangMasuk> tableData)
        {
            try
            {
                var model = new barangMasuk();
                foreach (var row in tableData)
                {
                    row.createdBy = Session["Username"].ToString();
                    row.createdDate = DateTime.Now;
                    db.barangMasuks.Add(row);
                    db.SaveChanges();
                }
               

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BarangKeluar/Edit/5
        public ActionResult EditModal(int id)
        {
            var model = (from s in db.barangMasuks
                         join b in db.barangs on s.idBarang equals b.id
                         join ss in db.suppliers on s.idSupllierBarang equals ss.id
                         where s.ID == id && b.deletedDate == null && ss.deletedDate == null
                         select new BarangMasukView
                         {
                             Id = s.ID,
                             TanggalMasuk = s.tanggalMasuk,
                             IdBarang = b.id,
                             NamaBarang = b.namaBarang,
                             JumlahBarang = s.jumlahBarang,
                             HargaBarang = b.hargaBarang,
                             HargaBarangTotal = b.hargaBarang * s.jumlahBarang,
                             Suplier = ss.namaSupplier,
                             satuan = b.satuan,
                             idSuplier = s.idSupllierBarang
                         }).FirstOrDefault();
            ViewBag.supplier = db.suppliers.Where(s => s.deletedDate == null && s.deletedBy == null).ToList();
            return PartialView("EditModal", model);
        }

        // POST: BarangKeluar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,int idSupllierBarang, barangMasuk model)
        {
            try
            {
                var data = db.barangMasuks.Find(id);

                data.tanggalMasuk = model.tanggalMasuk;
                data.jumlahBarang = model.jumlahBarang;
                data.idSupllierBarang = idSupllierBarang;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BarangKeluar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BarangKeluar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var data = db.barangMasuks.Find(id);
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
