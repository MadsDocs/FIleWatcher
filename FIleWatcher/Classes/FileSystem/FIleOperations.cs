using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Net;

using System.IO;
using FileWatcher.Classes.Logging;
using System.Security.Cryptography;

namespace FileWatcher.Classes.FileSystem
{
    class FIleOperations
    {
        private static Logger log = new Logger();

        public static string getFileExt( string path )
        {
            FileInfo fileext = new FileInfo(path);
            string ext = fileext.Extension;
            return ext;
        }


        public static string getAppSettings ( string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return config.AppSettings.Settings[key].Value;
        }

        public static string getLoggerDir ()
        {
            try
            {
                StreamReader loggerReader = new StreamReader(Classes.Statics.appdata + @"\FileWatcher\save");
                string content = loggerReader.ReadToEnd();

                return content;
            }
            catch ( Exception ex )
            {
                return " Cant find the save File!";
            }
        }

        public bool IsLongerthan260(string filename)
        {
            try
            {
                FileInfo fi = new FileInfo(filename);
                long flength = fi.Length;

                if (flength != Statics.max_length)
                    return false;
                else
                    return true;
            }
            catch ( Exception ex )
            {
                return false;
            }
        }

        public string Returnmd5Hash ( string filename)
        {
            if ( filename == string.Empty)
            {
                log._wLogger(" No Filename provided!");
                return null;
            }
            else
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                    }
                }
            }
        }

        public bool isDirEmpty ( string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

    }
}
