namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.barangKeluars", newName: "barangKeluar");
            RenameTable(name: "dbo.barangs", newName: "barang");
            CreateTable(
                "dbo.supplier",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaSupplier = c.String(),
                        alamatSupplier = c.String(),
                        contactSuplier = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.supplier");
            RenameTable(name: "dbo.barang", newName: "barangs");
            RenameTable(name: "dbo.barangKeluar", newName: "barangKeluars");
        }
    }
}
