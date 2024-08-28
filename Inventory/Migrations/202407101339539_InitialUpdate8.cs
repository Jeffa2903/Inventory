namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.costumer", "namaCustomer", c => c.String());
            DropColumn("dbo.costumer", "namaCuctomer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.costumer", "namaCuctomer", c => c.String());
            DropColumn("dbo.costumer", "namaCustomer");
        }
    }
}
