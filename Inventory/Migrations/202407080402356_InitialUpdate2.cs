namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.barangKeluars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        tanggalKeluar = c.DateTime(),
                        idBarang = c.Int(nullable: false),
                        jumlahBarang = c.Int(nullable: false),
                        hargaBarang = c.Decimal(nullable: false, precision: 18, scale: 2),
                        idSupllierBarang = c.Int(nullable: false),
                        createdBy = c.String(),
                        createdDate = c.DateTime(),
                        updatedBy = c.String(),
                        updatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.barangs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaBarang = c.String(),
                        satuan = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.barangMasuk", "hargaBarang", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.barangMasuk", "hargaBarang");
            DropTable("dbo.barangs");
            DropTable("dbo.barangKeluars");
        }
    }
}
