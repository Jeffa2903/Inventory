namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.barang", "jenis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.barang", "jenis");
        }
    }
}
