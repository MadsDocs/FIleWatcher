using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileWatcher.Classes.Logging;
using System.IO;

namespace FileWatcher.Classes
{
    class Statics
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static int max_length = 260;
    }
}
