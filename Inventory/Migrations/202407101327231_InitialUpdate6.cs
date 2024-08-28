namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.costumer",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaSupplier = c.String(),
                        alamatSupplier = c.String(),
                        contactSuplier = c.String(),
                        createdBy = c.String(),
                        createdDate = c.DateTime(),
                        deletedBy = c.String(),
                        deletedDate = c.DateTime(),
                        updatedBy = c.String(),
                        updatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            DropColumn("dbo.barangKeluar", "hargaBarang");
            DropColumn("dbo.barangMasuk", "hargaBarang");
        }
        
        public override void Down()
        {
            AddColumn("dbo.barangMasuk", "hargaBarang", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.barangKeluar", "hargaBarang", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.costumer");
        }
    }
}
