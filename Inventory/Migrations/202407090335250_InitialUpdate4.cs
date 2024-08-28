namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.barangKeluar", "deletedBy", c => c.String());
            AddColumn("dbo.barangKeluar", "deletedDate", c => c.DateTime());
            AddColumn("dbo.barangMasuk", "deletedBy", c => c.String());
            AddColumn("dbo.barangMasuk", "deletedDate", c => c.DateTime());
            AddColumn("dbo.barang", "createdBy", c => c.String());
            AddColumn("dbo.barang", "createdDate", c => c.DateTime());
            AddColumn("dbo.barang", "deletedBy", c => c.String());
            AddColumn("dbo.barang", "deletedDate", c => c.DateTime());
            AddColumn("dbo.barang", "updatedBy", c => c.String());
            AddColumn("dbo.barang", "updatedDate", c => c.DateTime());
            AddColumn("dbo.supplier", "createdBy", c => c.String());
            AddColumn("dbo.supplier", "createdDate", c => c.DateTime());
            AddColumn("dbo.supplier", "deletedBy", c => c.String());
            AddColumn("dbo.supplier", "deletedDate", c => c.DateTime());
            AddColumn("dbo.supplier", "updatedBy", c => c.String());
            AddColumn("dbo.supplier", "updatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.supplier", "updatedDate");
            DropColumn("dbo.supplier", "updatedBy");
            DropColumn("dbo.supplier", "deletedDate");
            DropColumn("dbo.supplier", "deletedBy");
            DropColumn("dbo.supplier", "createdDate");
            DropColumn("dbo.supplier", "createdBy");
            DropColumn("dbo.barang", "updatedDate");
            DropColumn("dbo.barang", "updatedBy");
            DropColumn("dbo.barang", "deletedDate");
            DropColumn("dbo.barang", "deletedBy");
            DropColumn("dbo.barang", "createdDate");
            DropColumn("dbo.barang", "createdBy");
            DropColumn("dbo.barangMasuk", "deletedDate");
            DropColumn("dbo.barangMasuk", "deletedBy");
            DropColumn("dbo.barangKeluar", "deletedDate");
            DropColumn("dbo.barangKeluar", "deletedBy");
        }
    }
}
