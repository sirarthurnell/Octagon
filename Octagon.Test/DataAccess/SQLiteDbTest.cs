using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octagon.Models;
using Octagon.Test.IO;
using SQLite;

namespace Octagon.Test.DataAccess
{
    [TestClass]
    public class SQLiteDbTest
    {
        private SQLiteAsyncConnection _db;
        private string _pathToDb;
        private string _dbName;

        public SQLiteDbTest()
        {
            _dbName = SQLiteConfiguration.GetFileName();
            _pathToDb = SQLiteConfiguration.GetFullFileName();
        }

        [TestInitialize]
        public void Init()
        {
            var sqlite = new SQLiteDb(_pathToDb);
            _db = sqlite.GetConnection();
        }

        [TestCleanup]
        public void TearDown()
        {
            _db.GetConnection().Close();

            var dbExists = File.Exists(_pathToDb);
            if (dbExists)
            {
                File.Delete(_pathToDb);
            }
        }

        [TestMethod]
        public void ShouldCreateDbFile()
        {
            var result = _db.CreateTableAsync<Checking>().Result;
            var exists = File.Exists(_pathToDb);

            Assert.IsTrue(exists);
        }
    }
}
