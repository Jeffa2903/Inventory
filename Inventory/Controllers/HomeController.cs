using Inventory.AppContext;
using Inventory.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Inventory.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            if(Session["Username"] == null){
                return RedirectToAction("Login", "Login");
            }
            string username = Session["Username"].ToString();
            string role = Session["Role"].ToString();
            ViewBag.username = username;
            ViewBag.role = role;

            var barang = db.barangs.Where(s => s.deletedBy == null).ToList();
            var supplier = db.suppliers.Where(s => s.deletedBy == null).ToList();
            var barangmasuk = from s in db.barangMasuks
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
            var barangKeluar = from s in db.barangKeluars
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
            ViewBag.Barang = barang.Count();
            ViewBag.Supplier = supplier.Count();
            ViewBag.BarangMasuk = barangmasuk.Count();
            ViewBag.BarangKeluar = barangKeluar.Count();
            var dashboardStock = from b in db.barangs
                                 join bm in db.barangMasuks
                                               .Where(x => x.deletedBy == null)
                                               .GroupBy(x => x.idBarang)
                                               .Select(g => new { IdBarang = g.Key, JumlahMasuk = g.Sum(x => x.jumlahBarang) })
                                               on b.id equals bm.IdBarang into bmGroup
                                 from bm in bmGroup.DefaultIfEmpty()
                                 join bk in db.barangKeluars
                                               .Where(x => x.deletedBy == null)
                                               .GroupBy(x => x.idBarang)
                                               .Select(g => new { IdBarang = g.Key, JumlahKeluar = g.Sum(x => x.jumlahBarang) })
                                               on b.id equals bk.IdBarang into bkGroup
                                 from bk in bkGroup.DefaultIfEmpty()
                                 where b.deletedBy == null
                                 select new BarangStock
                                 {
                                     idBarang = b.id,
                                     nameBarang = b.namaBarang.ToString(),
                                     jumlahMasuk = bm != null ? bm.JumlahMasuk : 0,
                                     jumlahKeluar = bk != null ? bk.JumlahKeluar : 0,
                                     stock = (bm != null ? bm.JumlahMasuk : 0) - (bk != null ? bk.JumlahKeluar : 0)
                                 };
            ViewBag.stok = dashboardStock.ToList();
            return View(dashboardStock);
        }

        public ActionResult downloadModal()
        {
            return PartialView("downloadModal");
        }


        [HttpPost]
        public ActionResult GenerateReport(DateTime startDate, DateTime endDate, string reportType)
        {

            List<BarangMasukView> reportData = new List<BarangMasukView>();
            string fileName = string.Empty;
            if (reportType == "barangMasuk")
            {
                reportData = (from s in db.barangMasuks
                              join b in db.barangs on s.idBarang equals b.id
                              join ss in db.suppliers on s.idSupllierBarang equals ss.id
                              where b.deletedDate == null && ss.deletedDate == null && s.deletedDate == null
                                    && s.tanggalMasuk >= startDate && s.tanggalMasuk <= endDate
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
                              }).ToList();
                fileName = "Laporan Barang Masuk.pdf";
            }
            else if (reportType == "barangKeluar")
            {
                reportData = (from k in db.barangKeluars
                              join b in db.barangs on k.idBarang equals b.id
                              join ss in db.costumers on k.idSupllierBarang equals ss.id
                              where b.deletedDate == null && ss.deletedDate == null && k.deletedDate == null
                                    && k.tanggalKeluar >= startDate && k.tanggalKeluar <= endDate
                              select new BarangMasukView
                              {
                                  Id = k.ID,
                                  TanggalMasuk = k.tanggalKeluar,
                                  IdBarang = b.id,
                                  NamaBarang = b.namaBarang,
                                  JumlahBarang = k.jumlahBarang,
                                  HargaBarang = b.hargaBarang,
                                  HargaBarangTotal = b.hargaBarang * k.jumlahBarang,
                                  Suplier = ss.namaCustomer,
                                  satuan = b.satuan,
                                  idSuplier = k.idSupllierBarang
                              }).ToList();
                fileName = "Laporan Barang Keluar.pdf";
            }

            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();

            Paragraph title = new Paragraph("INVENTORY REPORT", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Add Subtitle
            Paragraph subtitle = new Paragraph($"Laporan {(reportType == "barangMasuk" ? "Barang Masuk" : "Barang Keluar")}\nPeriode: {startDate.ToShortDateString()} - {endDate.ToShortDateString()}");
            subtitle.Alignment = Element.ALIGN_CENTER;
            document.Add(subtitle);
            document.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100; 
            table.SetWidths(new float[] { 5f, 10f, 15f, 10f, 15f, 20f, 20f });
            table.AddCell("No");
            table.AddCell("Nama Barang");
            table.AddCell("Supplier");
            table.AddCell("Jumlah");
            table.AddCell("Harga");
            table.AddCell("Total Harga");
            table.AddCell("Tanggal");
            int no = 0;
            foreach (var item in reportData)
            {
                no++;
                table.AddCell(no.ToString());
                table.AddCell(item.NamaBarang);
                table.AddCell(item.Suplier);
                table.AddCell(item.JumlahBarang.ToString());
                table.AddCell(item.HargaBarang.ToString());
                table.AddCell(item.HargaBarangTotal.ToString());
                table.AddCell(item.TanggalMasuk.ToString().Replace("00:00:00",""));
                
            }

            document.Add(table);
            document.Close();

            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            
            return File(byteArray, "application/pdf", fileName);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}