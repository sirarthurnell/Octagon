using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Octagon.Test.IO
{
    /// <summary>
    /// Contains several configurations for the creation
    /// of the SQLite database file.
    /// </summary>
    static class SQLiteConfiguration
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <returns>Path.</returns>
        public static string GetPath()
        {            
            string fullPath = Assembly.GetExecutingAssembly().Location;
            string normalizedPath = Path.GetDirectoryName(fullPath);
            return normalizedPath;
        }

        /// <summary>
        /// Gets the name of the SQLite file.
        /// </summary>
        /// <returns>Name of the SQLite file.</returns>
        public static string GetFileName()
        {
            return "octagon-v1.db3";
        }

        /// <summary>
        /// Gets the complete path to the SQLite file.
        /// </summary>
        /// <returns>Complete path to the SQLite file.</returns>
        public static string GetFullFileName()
        {
            return Path.Combine(GetPath(), GetFileName());
        }
    }
}
