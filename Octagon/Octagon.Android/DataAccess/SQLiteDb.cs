using System;
using System.IO;
using Octagon.DataAccess;
using Octagon.Droid.DataAccess;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace Octagon.Droid.DataAccess
{
    /// <summary>
    /// Represents a SQLite database file.
    /// </summary>
	public class SQLiteDb : ISQLiteDb
	{
        private static readonly string DbFileName = "octagon-v1.db3";

        /// <summary>
        /// Gets a connection to the SQLite DB.
        /// </summary>
        /// <returns></returns>
		public SQLiteAsyncConnection GetConnection()
		{
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, DbFileName);

            return new SQLiteAsyncConnection(path);
		}
	}
}

