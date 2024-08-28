namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.logins", newName: "login");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.login", newName: "logins");
        }
    }
}
