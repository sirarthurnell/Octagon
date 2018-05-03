using System;
using System.IO;
using Octagon.DataAccess;
using SQLite;

namespace Octagon.Test.DataAccess
{
    /// <summary>
    /// Represents a SQLite database file.
    /// </summary>
	public class SQLiteDb : ISQLiteDb
	{
        private string _pathToDb;

        /// <summary>
        /// Crea una nueva instancia de SQLiteDb.
        /// </summary>
        /// <param name="pathToDb">Path to the
        /// db file.</param>
        public SQLiteDb(string pathToDb)
        {
            _pathToDb = pathToDb;
        }

        /// <summary>
        /// Gets a connection to the SQLite DB.
        /// </summary>
        /// <returns></returns>
		public SQLiteAsyncConnection GetConnection()
		{
            return new SQLiteAsyncConnection(_pathToDb);
		}
	}
}

