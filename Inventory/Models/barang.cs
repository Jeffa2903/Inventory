using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class barang
    {
        public int id { get; set; }
        public string namaBarang { get; set; }
        public string satuan { get; set;}
        public decimal hargaBarang { get; set; }
        public string createdBy { get; set; }

        public string jenis { get; set; }
        public DateTime? createdDate { get; set; }
        public string deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}