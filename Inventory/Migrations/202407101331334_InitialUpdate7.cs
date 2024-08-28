namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.costumer", "namaCuctomer", c => c.String());
            AddColumn("dbo.costumer", "alamatCustomer", c => c.String());
            AddColumn("dbo.costumer", "contactCustomer", c => c.String());
            DropColumn("dbo.costumer", "namaSupplier");
            DropColumn("dbo.costumer", "alamatSupplier");
            DropColumn("dbo.costumer", "contactSuplier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.costumer", "contactSuplier", c => c.String());
            AddColumn("dbo.costumer", "alamatSupplier", c => c.String());
            AddColumn("dbo.costumer", "namaSupplier", c => c.String());
            DropColumn("dbo.costumer", "contactCustomer");
            DropColumn("dbo.costumer", "alamatCustomer");
            DropColumn("dbo.costumer", "namaCuctomer");
        }
    }
}
