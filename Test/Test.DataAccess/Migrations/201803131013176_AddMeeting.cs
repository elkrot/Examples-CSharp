namespace Test.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeeting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestEntityMeeting",
                c => new
                    {
                        TestEntity_TestKey = c.Int(nullable: false),
                        Meeting_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestEntity_TestKey, t.Meeting_Id })
                .ForeignKey("dbo.TestEntity", t => t.TestEntity_TestKey, cascadeDelete: true)
                .ForeignKey("dbo.Meeting", t => t.Meeting_Id, cascadeDelete: true)
                .Index(t => t.TestEntity_TestKey)
                .Index(t => t.Meeting_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestEntityMeeting", "Meeting_Id", "dbo.Meeting");
            DropForeignKey("dbo.TestEntityMeeting", "TestEntity_TestKey", "dbo.TestEntity");
            DropIndex("dbo.TestEntityMeeting", new[] { "Meeting_Id" });
            DropIndex("dbo.TestEntityMeeting", new[] { "TestEntity_TestKey" });
            DropTable("dbo.TestEntityMeeting");
            DropTable("dbo.Meeting");
        }
    }
}
