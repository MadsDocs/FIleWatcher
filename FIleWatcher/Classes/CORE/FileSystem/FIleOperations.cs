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
using System.Collections;

namespace FileWatcher.Classes.FileSystem
{
    class FIleOperations
    {
        private static Logger log = new Logger();
        private static string appdata = Classes.FileSystem.Init.appdata;
        private StringBuilder dirWatcher = new StringBuilder();
        private string line;



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

        public long getFileSize ( string filename )
        {
            try
            {


                if (string.IsNullOrEmpty(filename))
                {
                    return 0;
                }
                else
                {
                    FileInfo info = new FileInfo(filename);
                    long filesize = info.Length;

                    return filesize;
                }
            }
            catch ( Exception ex)
            {
                MessageBox.Show("Can´t find File: " + filename);
                log.ExLogger(ex);
                return 0;

            }
        }

        public bool createsettingsfile ( string path )
        {
            if ( string.IsNullOrEmpty(path))
            {
                return false;
            }
            else
            {
                if (File.Exists(path + @"\settings.settings"))
                {
                    File.Delete(path + @"\settings.settings");
                    StreamWriter settingswriter2 = new StreamWriter(path + @"\settings.settings");
                    settingswriter2.WriteLine("togglestatswindow = true");
                    settingswriter2.WriteLine("toggleentriewindow = true");
                    settingswriter2.WriteLine("notifyiffwdirisexists = true");
                    settingswriter2.Flush();
                    settingswriter2.Close();
                }
                else
                {

                    StreamWriter settingswriter = new StreamWriter(path + @"\settings.settings");
                    settingswriter.WriteLine("togglestatswindow = true");
                    settingswriter.WriteLine("toggleentriewindow = true");
                    settingswriter.Flush();
                    settingswriter.Close();
                }

                return true;
            }
        }

        public void writeFilterFile ()
        {
            // This Method will create a Filter File (FILEWATCH-14)
            if ( !File.Exists(Logger.Path + @"\filter.txt"))
            {
                File.Create(Logger.Path + @"\filter.txt");
            }
            else
            {
                log._wLogger("filter.txt exists, will not overwrite...");
            }

        }

        public ArrayList readFilterFile()
        {
            ArrayList filter = new ArrayList(10000);
            int counter = 0;
            try
            {
                if (!File.Exists(Logger.Path + @"\filter.txt"))
                {
                    log._wLogger("No Filter File found...");
                    return filter;
                }
                else
                {

                    StreamReader reader = new StreamReader(Logger.Path + @"\filter.txt");


                    while ((line = reader.ReadLine()) != null)
                    {
                        counter++;
                        filter.Add(line + @"\r\n");

                    }

                    log._wLogger("Found " + counter + "Filter Entries..");
                    return filter;

                }
            }
            catch ( Exception ex)
            {
                log._eLogger(ex);
                return filter;
            }
        }

    }
}
