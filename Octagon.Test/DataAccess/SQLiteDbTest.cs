using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octagon.Models;
using Octagon.Test.IO;
using SQLite;

namespace Octagon.Test.DataAccess
{
    [TestClass]
    public class SQLiteDbTest
    {
        private static SQLiteAsyncConnection _db;
        private static string _pathToDb = SQLiteConfiguration.GetFullFileName();
        private static string _dbName = SQLiteConfiguration.GetFileName();

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext context)
        {
            DeleteDb();
            await CreateDb();
        }

        private static void DeleteDb()
        {
            var dbExists = File.Exists(_pathToDb);
            if (dbExists)
            {
                File.Delete(_pathToDb);
            }
        }

        private static async Task CreateDb()
        {
            var sqlite = new SQLiteDb(_pathToDb);
            _db = sqlite.GetConnection();
            await _db.CreateTableAsync<Checking>();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if(_db != null)
            {
                _db.GetConnection().Close();
            }
        }

        [TestMethod]
        public void ShouldCreateDbFile()
        {
            var exists = File.Exists(_pathToDb);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task ShouldInsertThreeCheckingsAndRemoveAsync()
        {
            var checkingsToInsert = new List<Checking>
            {
                new Checking
                {
                    Timestamp = new DateTime(2018, 5, 2, 17, 0, 0),
                    Direction = CheckingDirection.Out
                },
                new Checking
                {
                    Timestamp = new DateTime(2018, 5, 3, 17, 0, 0),
                    Direction = CheckingDirection.In
                },
                new Checking
                {
                    Timestamp = new DateTime(2018, 5, 4, 17, 0, 0),
                    Direction = CheckingDirection.Out
                }
            };

            await _db.InsertAllAsync(checkingsToInsert);
            var insertedCheckings = await _db.Table<Checking>()
                .ToListAsync();

            Assert.AreEqual(checkingsToInsert.Count, insertedCheckings.Count);

            foreach (var checking in insertedCheckings)
            {
                await _db.DeleteAsync(checking);
            }

            var existentCheckings = await _db.Table<Checking>()
                .ToListAsync();

            Assert.AreEqual(0, existentCheckings.Count);
        }

        [TestMethod]
        public async Task ShouldInsertWithIdAndRecoverSameCheking()
        {
            var timestamp = new DateTime(2018, 5, 2, 17, 0, 0);
            var checkingToInsert = CreateFakeChecking(timestamp);

            //Id is modified in place after insertion by SQLite PCL.
            var rowsInsertedCount = await _db.InsertAsync(checkingToInsert);
            var insertedChecking = await _db.Table<Checking>()
                .Where(checking => checking.Id == checkingToInsert.Id)
                .FirstOrDefaultAsync();

            await _db.DeleteAsync(insertedChecking);

            Assert.IsNotNull(insertedChecking);
            Assert.AreNotEqual(0, insertedChecking.Id);
        }

        [TestMethod]
        public async Task ShouldInsertAndRecoverUtcDateTime()
        {
            var timestamp = new DateTime(2018, 5, 2, 17, 0, 0, DateTimeKind.Utc);
            var checkingToInsert = CreateFakeChecking(timestamp);

            await _db.InsertAsync(checkingToInsert);
            var insertedChecking = await _db.Table<Checking>()
                .Where(checking => checking.Timestamp == timestamp)
                .FirstOrDefaultAsync();

            await _db.DeleteAsync(insertedChecking);

            Assert.IsNotNull(insertedChecking);
        }

        [TestMethod]
        public async Task ShouldRecoverMostRecentChecking()
        {
            var beforeTimestamp = new DateTime(2018, 5, 2, 17, 0, 0, DateTimeKind.Utc);
            var afterTimestamp = new DateTime(2019, 5, 2, 17, 0, 0, DateTimeKind.Utc);
            var fromTimestamp = new DateTime(2019, 1, 1, 0, 0, 0);
            var checkingsToInsert = new List<Checking>
            {
                CreateFakeChecking(beforeTimestamp),
                CreateFakeChecking(afterTimestamp)
            };

            await _db.InsertAllAsync(checkingsToInsert);
            var insertedCheckings = await _db.Table<Checking>()
                .Where(checking => checking.Timestamp > fromTimestamp)
                .ToListAsync();

            foreach(var insertedChecking in insertedCheckings)
            {
                await _db.DeleteAsync(insertedChecking);
            }

            Assert.IsTrue(insertedCheckings.Count == 1);
            Assert.IsNotNull(insertedCheckings);
        }

        /// <summary>
        /// Creates a fake checking.
        /// </summary>
        /// <param name="timestamp">Timestamp of
        /// the checking.</param>
        /// <returns>Fake checking.</returns>
        private Checking CreateFakeChecking(DateTime timestamp)
        {
            return new Checking
            {
                Id = 0,
                Timestamp = timestamp,
                Direction = CheckingDirection.Out
            };
        }
    }
}
