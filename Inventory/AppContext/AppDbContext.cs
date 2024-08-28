using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Inventory.AppContext
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<login> Logins { get; set; }
        public DbSet<barangMasuk> barangMasuks{ get; set; }
        public DbSet<barangKeluar> barangKeluars{ get; set; }
        public DbSet<barang> barangs { get; set; }
        public DbSet<supplier> suppliers { get; set; }
        public DbSet<costumer> costumers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Contoh konfigurasi penamaan tabel
            modelBuilder.Entity<login>().ToTable("login");
            modelBuilder.Entity<barangMasuk>().ToTable("barangMasuk");
            modelBuilder.Entity<barangKeluar>().ToTable("barangKeluar");
            modelBuilder.Entity<barang>().ToTable("barang");
            modelBuilder.Entity<supplier>().ToTable("supplier");
            modelBuilder.Entity<costumer>().ToTable("costumer");

        }

    }

    
}