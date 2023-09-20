using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Classes
{
    class Statics
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static int max_length = 260;
        public bool emptydir = false;
        public static string pathToSqlFile = appdata + @"\FileWatcher\Database\entries.sqlite";
        public static string Internalconnectionstring = "Data Source=MyDatabase.sqlite;Version=3;";
    }
}
