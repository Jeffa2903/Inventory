using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class BarangStock
    {
        public int idBarang { get; set; }
        public string nameBarang { get; set; }
        public int jumlahMasuk { get; set; }
        public int jumlahKeluar { get; set; }
        public int stock { get; set;}
    }
}