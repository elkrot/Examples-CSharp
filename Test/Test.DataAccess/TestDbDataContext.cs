using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Test.Model;
namespace Test.DataAccess
{
    public class TestDbDataContext:DbContext
    {
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        public TestDbDataContext():base("TestDbDataContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
