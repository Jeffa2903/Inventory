using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class costumer
    {
        public int id { get; set; }

        public string namaCustomer { get; set; }
        public string alamatCustomer { get; set; }
        public string contactCustomer { get; set; }

        public string createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public string deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}