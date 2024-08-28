using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class barangKeluar
    {
        public int ID { get; set; }
        public DateTime? tanggalKeluar { get; set; }
        public int idBarang { get; set; }
        public int jumlahBarang { get; set; }

        public int idSupllierBarang { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }

        public string deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }


    }
}