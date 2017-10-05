using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestDataGenerator.BL;
using TestDataGenerator.Data;

namespace TestDataGenerator.UnitTests
{
    [TestClass]
    public class ScriptGenerator_Test
    {
        ScriptGenerator generator;
        [TestInitialize]
        public void Init()
        {
            IRepository mock = new RepositoryMock();
            generator = new ScriptGenerator(mock);
        }

        [TestMethod]
        public void GenerateUser_NameRequired()
        {
            // Arrange
           // IScriptGenerator generator = null;

            //Act
            UserEntity entity = generator.GenerateUser();
            string name = entity.Name;

            //Assert
            Assert.IsNotNull(entity.Name);

        }

        [TestMethod]
        public void GenerateUser_SurnameRequired()
        {
            

            //Act
            UserEntity entity = generator.GenerateUser();
            string name = entity.SurName;

            //Assert
            Assert.IsNotNull(entity.SurName);

        }

        [TestMethod]
        public void GenerateUser_LoginRequired()
        {
            

            //Act
            UserEntity entity = generator.GenerateUser();
            string name = entity.Login;

            //Assert
            Assert.IsNotNull(entity.Login);

        }


        [TestMethod]
        public void GenerateUser_PasswordRequired()
        {
          

            //Act
            UserEntity entity = generator.GenerateUser();
            string name = entity.Password;

            //Assert
            Assert.IsNotNull(entity.Password);

        }

        [TestMethod]
        public void GenerateUser_EmailRequired()
        {
          
            //Act
            UserEntity entity = generator.GenerateUser();
            string name = entity.Email;

            //Assert
            Assert.IsNotNull(entity.Email);

        }


        [TestMethod]
        
        public void GenerateUser_RegistrationDatePeriod()
        {

            //Act
            UserEntity entity = generator.GenerateUser();
            DateTime dt = entity.RegistrationDate;

            //Assert
           
            Assert.IsTrue(dt>new DateTime(2017,1,1));

        }

        [TestMethod]
        public void GenerateUser_GetValueLine()
        {

            UserEntity user = new UserEntity()
            {
                Name="Петр"
                ,SurName="Петров"
                ,Patronymic="Петрович"
                ,Email="petr@gmail.com"
                ,Login="petr"
                ,Password="12345"
                ,RegistrationDate =new DateTime(2016,1,1)
            };

            const string EXPECTED_RESULT =
                @"VALUES ('Петр','Петров','Петрович','petr@gmail.com','petr','12345',)";

            //Act
            string result = generator.GetValueLine(user);
            

            //Assert
            Assert.AreEqual(EXPECTED_RESULT,result);

        }

        [TestMethod]
        public void GenerateUser_GetInsertLine()
        {
            const string EXPECTED_RESULT = @"INSERT INTO BlogUser (Name,Surname,Patronymic,Email,Login,Password,RegistrationDate)";
            //Act
            UserEntity user = new UserEntity()
            {
                Name = "Петр"
,
                SurName = "Петров"
,
                Patronymic = "Петрович"
,
                Email = "petr@gmail.com"
,
                Login = "petr"
,
                Password = "12345"
,
                RegistrationDate = new DateTime(2016, 1, 1)
            };
            string result = generator.GetValueLine(user);
            

            //Assert
            Assert.Equals(result, EXPECTED_RESULT);

        }
        [TestMethod]
        public void MergeLines_test() {
            const string INSERT_LINE = "INSERT LINE";
            string[] valueLines = { "Line1", "Line2" };
            string expecteResult = string.Format("INSERT LINE{0}value Line1", Environment.NewLine);
            string result = generator.MergeLines(valueLines, INSERT_LINE);
        }

        /*
          private EmailValidationLogic _validationLogic;

        //Optional, rarely used. Executes before all tests in the assembly are run
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
        }

        //Optional, rarely used. Runs once before all the tests in this class run
        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        { 
        }

        //This code runs before each test
        [TestInitialize]
        public void Setup()
        {
            _validationLogic = new EmailValidationLogic();
        }


        //Optional, rarely used. Runs once after all tests in the assembly are run
        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
        }

        //Optional, rarely used. This runs once after all the tests in this class are run
        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
        }

        //This is code runs after each test
        [TestCleanup]
        public void TearDown()
        { 
        }

        //Example Test Method
        [TestMethod]
        public void EmailShouldNotHaveMoreThanOneAtSymbol()
        {
            //Arrange
            bool expected = false;

            //Act
            bool result = _validationLogic.IsEmailValid("jsmith@@hotmail.com");

            //Assert
            Assert.AreEqual(expected, result, "Email should not have more than one @");
        }

        //Test Exception Being Thrown
        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException))]
        public void DivideMethodTest()
        {
            int x = 1;
            int y = x/0;
        }





        Пространство имен Microsoft.VisualStudio.TestTools.UnitTesting предоставляет несколько типов классов Assert.
Assert
В методе теста можно вызывать любое число методов класса Assert, таких как Assert.AreEqual(). Класс Assert содержит много методов для выбора, и многие из этих методов имеют несколько перегрузок.
CollectionAssert
Класс CollectionAssert служит для сравнения коллекций объектов и проверки состояния одной или нескольких коллекций.
StringAssert
Класс StringAssert служит для сравнения строк. Этот класс содержит различные полезные методы, такие как such as StringAssert.Contains, StringAssert.Matches и StringAssert.StartsWith.
AssertFailedException
Исключение AssertFailedException возникает в случае невыполнения теста. Причиной невозможности выполнения теста может быть истечение времени ожидания, непредвиденное исключение или оператор Assert, создающий результат "Ошибка".
AssertInconclusiveException
Исключение AssertInconclusiveException возникает при каждом результате теста с неопределенным результатом. Как правило, оператор Assert.Inconclusive добавляется к тесту, над которым еще ведется работа, для обозначения его неготовности к выполнению.
System_CAPS_ICON_note.jpg Примечание
Альтернативным вариантом может быть обозначение теста, который еще не готов к выполнению, атрибутом Ignore. Однако, недостатком в этом случае является невозможность простого создания отчета по числу тестов, которые еще необходимо реализовать.
UnitTestAssertException
При написании нового класса исключения Assert наследование этого класса от базового класса UnitTestAssertException упрощает выявление исключения как ошибки подтверждения, а не непредвиденного исключения, выдаваемого тестом или продуктивным кодом.
ExpectedExceptionAttribute
Если необходимо, чтобы метод теста проверял, что исключение, возникающее в этом методе, на самом деле является требуемым исключением, включите в метод теста атрибут ExpectedExceptionAttribute.
         */
    }
}
