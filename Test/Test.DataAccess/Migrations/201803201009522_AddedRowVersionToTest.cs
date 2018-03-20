namespace Test.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRowVersionToTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestEntity", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestEntity", "RowVersion");
        }
    }
}
