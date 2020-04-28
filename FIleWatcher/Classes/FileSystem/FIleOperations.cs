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

using System.Security.Principal;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileWatcher.Classes.FileSystem
{
    class FIleOperations
    {
        private static Logger log = new Logger();
        private static string appdata = Classes.FileSystem.Init.appdata;



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
                StreamReader loggerReader = new StreamReader(appdata + @"\FileWatcher\save");
                string content = loggerReader.ReadToEnd();

                if (!Directory.Exists(content))
                {
                    DialogResult result = MessageBox.Show("Directory not found, do you want to edit the save File?", "Directory not found", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                    {
                        Process.Start("notepad.exe", appdata + @"\FileWatcher\save");
                        return "";
                    }
                    else
                    {
                        MessageBox.Show(@"To continue with the Programm please edit the save File under %appdata%\FileWatcher\save", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(-1);
                        return "";
                    }
                }
                else
                    return content;
            }
            catch ( Exception ex )
            {
                return MessageBox.Show(ex.Message + ex.StackTrace, "Can´t find SAVE FILE", MessageBoxButtons.OK, MessageBoxIcon.Error).ToString();
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
                log.ExLogger(ex);
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

        /// <summary>
        /// Diese Methode gibt den Benutzer einer Datei zurück
        /// </summary>
        /// <param name="Filename"></param>
        /// <returns></returns>
        public string GetOwnerofFile ( string Filename)
        {
            try
            {
                FileInfo info = new FileInfo(Filename);
                var owner = info.GetAccessControl().GetOwner(typeof(NTAccount));
                return owner.ToString();
            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
                return "FEHLER!";
            }
        }
    }
}
