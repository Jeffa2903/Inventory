using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
 
        public class BarangMasukView
        {
            public int Id { get; set; }
            public DateTime? TanggalMasuk { get; set; }
            public int IdBarang { get; set; }
            public string NamaBarang { get; set; }
            public int JumlahBarang { get; set; }
            public decimal HargaBarang { get; set; }
            public decimal HargaBarangTotal { get; set; }
            public string Suplier { get; set; }
            public string satuan { get; set; }
            public int idSuplier { get; set;}
        }

}