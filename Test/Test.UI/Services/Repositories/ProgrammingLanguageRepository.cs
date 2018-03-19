using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Model;
using Test.UI.Services.Repositories;

namespace Test.UI.Services.Repositories
{
    public class ProgrammingLanguageRepository : GenericRepository<ProgrammingLanguage, TestDbDataContext> ,IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(TestDbDataContext context) : base(context)
        {
        }
    }
}
