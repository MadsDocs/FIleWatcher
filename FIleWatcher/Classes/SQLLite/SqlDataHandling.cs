using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileWatcher.Classes.Logging;
using System.IO;
using System.Data.SQLite;

namespace FileWatcher.Classes.SQLLite
{
    internal class SqlDataHandling
    {
        Logger log = new Logger();
        public static void insertData ( string Data)
        {
            
        }

        public bool checkIfTableExists (string table)
        {
            //Checks if the given table exists.
            if (checkIfDatabaseExists(Statics.pathToSqlFile))
            {
                string s_checkcreatetable = "SELECT * FROM ";
                SQLiteConnection con = new SQLiteConnection(Statics.Internalconnectionstring);

                try
                {
                    con.Open();

                }
                catch
                {
                    return false;
                }
            }
        }

        private bool checkIfDatabaseExists(string path)
        {
            if (path == string.Empty)
            {
                log._wLogger("No Given path in checkIfDatabaseExists");
                return false;
            }
            else
            {
                if (File.Exists(path))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void createDatabaseFile(string path)
        {
            if (!File.Exists(path)){
                SQLiteConnection.CreateFile(Statics.pathToSqlFile);
            }
            else
            {
                Logger log = new Logger();
                log._wLogger("Database already exists");
            }
            
        }
    }
}
