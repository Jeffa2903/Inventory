using Inventory.AppContext;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Inventory.Controllers
{
    public class BarangKeluarController : Controller
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
            ViewBag.Menu = "Barang Keluar";
            var model = from s in db.barangKeluars
                        join b in db.barangs
                        on s.idBarang equals b.id
                        join ss in db.costumers on s.idSupllierBarang equals ss.id
                        where b.deletedDate == null && ss.deletedDate == null && s.deletedDate == null
                        select new BarangMasukView

                        {
                            Id = s.ID,
                            TanggalMasuk = s.tanggalKeluar,
                            IdBarang = b.id,
                            NamaBarang = b.namaBarang,
                            JumlahBarang = s.jumlahBarang,
                            HargaBarang = b.hargaBarang,
                            HargaBarangTotal = b.hargaBarang * s.jumlahBarang,
                            Suplier = ss.namaCustomer,
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
            ViewBag.Menu = "Input Data Barang Keluar";
            ViewBag.supplier = db.costumers.Where(s => s.deletedDate == null && s.deletedBy == null).ToList();
            ViewBag.barang = db.barangs.Where(s => s.deletedDate == null && s.deletedBy == null).ToList();

            var stok = from b in db.barangs
                       join bm in db.barangMasuks.GroupBy(x => x.idBarang)
                                                 .Select(g => new { IdBarang = g.Key, JumlahMasuk = g.Sum(x => x.jumlahBarang) })
                                                 on b.id equals bm.IdBarang into bmGroup
                       from bm in bmGroup.DefaultIfEmpty()
                       join bk in db.barangKeluars.GroupBy(x => x.idBarang)
                                                 .Select(g => new { IdBarang = g.Key, JumlahKeluar = g.Sum(x => x.jumlahBarang) })
                                                 on b.id equals bk.IdBarang into bkGroup
                       from bk in bkGroup.DefaultIfEmpty()
                       select new BarangStock
                       {
                           idBarang = b.id,
                           stock = (bm != null ? bm.JumlahMasuk : 0) - (bk != null ? bk.JumlahKeluar : 0)
                       };

            foreach (var b in stok.ToList())
            {
                string s = b.stock.ToString();
                string i = b.idBarang.ToString();
            }
            ViewBag.Stok = stok.ToList();

            foreach(var item in ViewBag.Stok){
                string s = item.stock.ToString();
                string i = item.idBarang.ToString();
            }

            return View();
        }

        // POST: BarangKeluar/Create
        [HttpPost]
        public ActionResult CreateSave(List<barangKeluar> tableData)
        {
            try
            {
                var model = new barangKeluar();

                foreach(var row in tableData)
                {
                    
                    row.createdBy = Session["Username"].ToString();
                    row.createdDate = DateTime.Now;
                    db.barangKeluars.Add(row);
                    db.SaveChanges();
                }
                
                // TODO: Add insert logic here

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
            var model = (from s in db.barangKeluars
                         join b in db.barangs on s.idBarang equals b.id
                         join ss in db.costumers on s.idSupllierBarang equals ss.id
                         where s.ID == id && b.deletedDate == null && ss.deletedDate == null
                         select new BarangMasukView
                         {
                             Id = s.ID,
                             TanggalMasuk = s.tanggalKeluar,
                             IdBarang = b.id,
                             NamaBarang = b.namaBarang,
                             JumlahBarang = s.jumlahBarang,
                             HargaBarang = b.hargaBarang,
                             HargaBarangTotal = b.hargaBarang * s.jumlahBarang,
                             Suplier = ss.namaCustomer,
                             satuan = b.satuan,
                             idSuplier = s.idSupllierBarang
                         }).FirstOrDefault();
            ViewBag.supplier = db.suppliers.Where(s => s.deletedDate == null && s.deletedBy == null).ToList();
            return PartialView("EditModal", model);
        }

        // POST: BarangKeluar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int idSupllierBarang, barangKeluar model, BarangMasukView barang)
        {
            try
            {
                // TODO: Add update logic here
                var data = db.barangKeluars.Find(id);

                data.tanggalKeluar = model.tanggalKeluar;
                data.jumlahBarang = model.jumlahBarang;
                data.idSupllierBarang = idSupllierBarang;
                if (barang.TanggalMasuk != null)
                {
                    data.tanggalKeluar = barang.TanggalMasuk;
                }
                
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
                var data = db.barangKeluars.Find(id);
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
