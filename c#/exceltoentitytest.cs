using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManager.Infrastructure.CrossCutting;
namespace OrderManagerTest
{
    /// <summary>
    /// ExcelToEntityTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ExcelToEntityTest
    {
        public ExcelToEntityTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestImportExcel()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
            ExcelToEntityByNpoi<Person> personExcel = new ExcelToEntityByNpoi<Person>();
            personExcel.InitializeWorkbook(@"d:\person.xls");
            List<string> namelist = new List<string>();
            namelist.Add("Name");
            namelist.Add("Sex");
            var persons=personExcel.ImportToEntity(namelist);
            Assert.IsTrue(persons.Count == 3);

        }
        [TestMethod]
        public void TestImportExcel1()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
            ExcelToEntityByNpoi<Testclass> personExcel = new ExcelToEntityByNpoi<Testclass>();
            personExcel.InitializeWorkbook(@"d:\person.xls");
            List<string> namelist = new List<string>();
            namelist.Add("T2");
           
            var persons = personExcel.ImportToEntity(namelist);
            Assert.IsTrue(persons.Count == 3);

        }
        public class Person
        {
            public string Name { get; set; }
            public string Sex { get; set; }
            
        }
        public class Testclass
        {
            public string T1 { get; set; }
            public string T2 { get; set; }
        }
    }
}
