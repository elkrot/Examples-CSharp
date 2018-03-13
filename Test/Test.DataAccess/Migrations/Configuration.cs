namespace Test.DataAccess.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Test.DataAccess.TestDbDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Test.DataAccess.TestDbDataContext context)
        {
            context.Tests.AddOrUpdate(f => f.TestTitle, new Model.TestEntity() { TestTitle = "Test1" },
                new Model.TestEntity() { TestTitle = "Test2" }, new Model.TestEntity() { TestTitle = "Test3" });

            context.ProgrammingLanguages.AddOrUpdate(l => l.Name, new Model.ProgrammingLanguage() { Name = "C#" }
            , new Model.ProgrammingLanguage() { Name = "C++" }, new Model.ProgrammingLanguage() { Name = "Java" });
            context.SaveChanges();
            context.Questions.AddOrUpdate
                            (l => l.QuestionTitle, new Model.Question() { QuestionTitle = "Q1", TestKey = context.Tests.First().TestKey }
                        , new Model.Question() { QuestionTitle = "Q2", TestKey = context.Tests.First().TestKey });
            context.Meetings.AddOrUpdate(m => m.Title,
                new Meeting
                {
                    Title = "Watching Soccer",
                    DateFrom = new DateTime(2018, 5, 26),
                    DateTo = new DateTime(2018, 5, 26),
                    Tests = new List<TestEntity>{
                        context.Tests.Single(t=>t.TestTitle=="Test1"),
                        context.Tests.Single(t=>t.TestTitle=="Test2")
                    }
                });

        }
    }
}
