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
        private StringBuilder dirWatcher = new StringBuilder();



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

                string extenstion = info.Extension;
                extenstion.ToLower();
                switch (extenstion)
                {
                    case ".pf":
                    case ".tmp":
                    case ".lock":
                    case "*.lock":
                    case ".xml.lock":
                    case "db-wal":
                        log._wLogger("Hitting the Great Filter, arrrghhhhhh");
                        return "Hitting Filter!";
                    default:
                        return "";
                }

            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
                log._wLogger(Filename);
                return "FEHLER!";
            }
        }

        public void SaveDirHistory (string path)
        {
            if ( path == string.Empty)
            {
                log._wLogger("No Path given!");
            }
            else
            {
                string loggerdir = getLoggerDir();

                if (loggerdir == string.Empty)
                {
                    log._wLogger("No Path given!");
                }
                else
                {
                    dirWatcher.Append(path + "\r\n");
                    File.AppendAllText(loggerdir + @"\dirhistory.log", dirWatcher.ToString());
                }

            }
        }

        public void deleteFile ( string filename )
        {
            try
            {


                if (filename == string.Empty)
                {
                    log._wLogger("Can´t delete an empty or non-existent File!");
                }
                else
                {
                    File.Delete(filename);
                    log._wLogger("Deleted File: " + filename);
                }
            }
            catch ( IOException ioe)
            {
                log.ExLogger(ioe);
            }
        }

        public bool checkLastWriteFile ( string filename)
        {
            //Checks if a File was modified...
            try
            {
                if (string.IsNullOrEmpty(filename))
                {
                    log._wLogger("Can´t access File!");
                    return false;
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    DateTime currentModifed = fileInfo.LastWriteTime;
                    DateTime currentTime = DateTime.Now;

                    if (currentModifed != currentTime)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    


                }



            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
                return false;
            }
        }

    }
}
