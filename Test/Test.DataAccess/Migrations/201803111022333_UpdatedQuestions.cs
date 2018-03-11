namespace Test.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedQuestions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Question", "QuestionTitle", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Question", "QuestionTitle", c => c.String());
        }
    }
}
