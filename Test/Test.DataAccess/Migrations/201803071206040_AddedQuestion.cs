namespace Test.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionTitle = c.String(),
                        TestKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestEntity", t => t.TestKey, cascadeDelete: true)
                .Index(t => t.TestKey);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Question", "TestKey", "dbo.TestEntity");
            DropIndex("dbo.Question", new[] { "TestKey" });
            DropTable("dbo.Question");
        }
    }
}
