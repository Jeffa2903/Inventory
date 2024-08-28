namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.barangMasuk",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        tanggalMasuk = c.DateTime(),
                        idBarang = c.Int(nullable: false),
                        jumlahBarang = c.Int(nullable: false),
                        idSupllierBarang = c.Int(nullable: false),
                        createdBy = c.String(),
                        createdDate = c.DateTime(),
                        updatedBy = c.String(),
                        updatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.barangMasuk");
        }
    }
}
