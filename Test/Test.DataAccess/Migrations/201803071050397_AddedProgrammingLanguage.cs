namespace Test.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProgrammingLanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TestEntity", "FavoriteLanguageId", c => c.Int());
            CreateIndex("dbo.TestEntity", "FavoriteLanguageId");
            AddForeignKey("dbo.TestEntity", "FavoriteLanguageId", "dbo.ProgrammingLanguage", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestEntity", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropIndex("dbo.TestEntity", new[] { "FavoriteLanguageId" });
            DropColumn("dbo.TestEntity", "FavoriteLanguageId");
            DropTable("dbo.ProgrammingLanguage");
        }
    }
}
