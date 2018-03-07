namespace Test.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestEntity",
                c => new
                    {
                        TestKey = c.Int(nullable: false, identity: true),
                        TestTitle = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TestKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestEntity");
        }
    }
}
