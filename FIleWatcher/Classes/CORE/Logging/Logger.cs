using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

using System.Security.Principal;
using FileWatcher;
using System.Threading;

namespace FileWatcher.Classes.Logging
{
   class Logger
    {
        public static string currentdir = Environment.CurrentDirectory;
        private static StringBuilder sb = new StringBuilder();
        private static StringBuilder sb2 = new StringBuilder();
        private static StringBuilder error = new StringBuilder();
        private static StringBuilder sb3 = new StringBuilder();

        public static string Path = MainWindow.pfad;
        //public static string Path { get => path; set => path = value; }
        

        /// <summary>
        /// Dies ist der Error Logger
        /// </summary>
        /// <param name="ex"></param>
        public void _eLogger(Exception ex)
        {
            try
            {
                if (!Directory.Exists(Statics.appdata + @"\FileWatcher"))
                {
                    MessageBox.Show("There is no FileWatcher Directory, please restart FileWatcher, or create a new Direcotry under %appdata% named FileWatcher!", "No FileWatcher Directory", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    sb.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "| ERROR |" +  "\t" + ex.Message + "\r\n").ToString();
                    File.AppendAllText(Statics.appdata + @"\FileWatcher\Logs\log.log", sb.ToString());
                    sb.Clear();
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        
        public void ExLogger ( Exception ex)
        {
            try
            {
                if (!Directory.Exists(Statics.appdata + @"\FileWatcher"))
                {
                    MessageBox.Show("There is no FileWatcher Directory, please restart the FileWatcher, or create a new Direcotry under %appdata% named FileWatcher!", "No FileWatcher Directory", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string fwpath = GetPath();

                    error.Append(DateTime.Now.ToLongDateString() + "\t" + ex.Message + "\r\n") ;
                    error.Append(DateTime.Now.ToLongDateString() + "\t" + ex.StackTrace + "\r\n");

                    File.AppendAllText(fwpath + @"\error.log", error.ToString()  + "\r\n");
                    error.Clear();
                }
            }
            catch ( Exception exe)
            {
                MessageBox.Show(@"Can´t write into the Log File, please check the save file under %appdata%\FileWatcher\save" + "\t" + exe.Message + exe.StackTrace
                                , "Error while Logging", MessageBoxButton.OK, MessageBoxImage.Error); ;
            }
        }

        /// <summary>
        /// Dies ist der Warning Logger
        /// </summary>
        /// <param name="message"></param>
        public void _wLogger(string message)
        {
            try
            {

                if (!Directory.Exists(Statics.appdata + @"\FileWatcher"))
                {
                    MessageBox.Show("There is no FileWatcher Directory, please restart the FileWatcher, or create a new Direcotry under %appdata% named FileWatcher!", "No FileWatcher Directory", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    sb.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + message + "\r\n").ToString();
                    File.AppendAllText(Statics.appdata + @"\FileWatcher\Logs\log.log", sb.ToString());
                    sb.Clear();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Logger.Path + @"\log.log", ex.Message + "\r\n");
            }
        }
        public void LogEntrys (string name, DateTime time, WatcherChangeTypes types)
        {
            try
            {
                FileInfo attributes = new FileInfo(name);
                DateTime creationTime = attributes.CreationTimeUtc;
                string fattributes = attributes.Attributes.ToString();

                var besitzer = File.GetAccessControl(name).GetOwner(typeof(NTAccount));
                var gruppe = File.GetAccessControl(name).GetGroup(typeof(NTAccount));

                if (attributes.Length != Statics.max_length)
                {

                    string extension = attributes.Extension;

                    if (extension == ".tmp")
                    {
                        if (Path == "INVALID" || Path == string.Empty)
                        {
                            _wLogger("Kann Entries.log nicht schreiben da der Pfad invalid ist!");
                        }
                        else
                        {
                            sb2.Append(DateTime.Now.Date.ToLongDateString() + " "+ DateTime.Now.ToLongTimeString() + "\r\n");
                            sb2.Append(name + "\r\n");
                            sb2.Append("Changed to Type: " + types + "\r\n");
                            sb2.Append("Creation Time: " + creationTime.ToString() + "\r\n");
                            sb2.Append("Attributes: " + fattributes + "\r\n");
                            sb2.Append("Owner: " + besitzer + "\r\n");
                            sb2.Append("Group: " + gruppe + "\r\n");
                            sb2.Append("\r\n");
                            File.AppendAllText(Path + @"\entries.log", sb2.ToString());
                            sb2.Clear();
                        }
                    }
                    else
                    {
                        if (Path == "INVALID" || Path == string.Empty)
                        {
                            _wLogger("Kann Entries.log nicht schreiben da der Pfad invalid ist!");
                        }
                        else
                        {
                            long length2 = attributes.Length;
                            sb2.Append(DateTime.Now.Date.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n");
                            sb2.Append(name + "\r\n");
                            sb2.Append("Changed to Type: " + types + "\r\n");
                            sb2.Append("Creation Time: " + creationTime.ToString() + "\r\n");
                            sb2.Append("Attributes: " + fattributes + "\r\n");
                            sb2.Append("Length: " + length2 + "\r\n");
                            sb2.Append("Owner: " + besitzer + "\r\n");
                            sb2.Append("Group: " + gruppe + "\r\n");
                            sb2.Append("\r\n");
                            File.AppendAllText(Path + @"\entries.log", sb2.ToString());
                            sb2.Clear();
                        }
                    }
                }
                else
                {
                    sb2.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "== Datei wurde nicht mitgeloggt! " + attributes.Name + " == ");
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Logger.Path + @"\log.log", ex.Message + "\r\n");
            }

        }

        public void LogDirEntrys(string name, DateTime time, WatcherChangeTypes types)
        {
            try
            {
                FileInfo attributes = new FileInfo(name);
                DateTime creationTime = attributes.CreationTimeUtc;

                string fattributes = attributes.Attributes.ToString();

               var besitzer = File.GetAccessControl(name).GetOwner(typeof(NTAccount));
               var gruppe = File.GetAccessControl(name).GetGroup(typeof(NTAccount));



                string extension = attributes.Extension;

                if (extension == ".tmp")
                {
                    if (Path == "INVALID" || Path == string.Empty)
                    {
                        _wLogger("Kann Entries.log nicht schreiben da der Pfad invalid ist!");
                    }
                    else
                    {
                        sb3.Append(DateTime.Now.Date.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\t" + "= DirectoryWatcher =" + "\r\n");
                        sb3.Append(name + "\r\n");
                        sb3.Append("Changed to Type: " + types + "\r\n");
                        sb3.Append("Creation Time: " + creationTime.ToString() + "\r\n");
                        sb3.Append("Attributes: " + fattributes + "\r\n");
                        sb3.Append("Owner: " + besitzer + "\r\n");
                        sb3.Append("Group: " + gruppe + "\r\n");

                        sb3.Append("\r\n");
                        File.AppendAllText(Path + @"\direntries.log", sb3.ToString());
                        sb3.Clear();
                    }
                }
                else
                {
                    if (Path == "INVALID" || Path == string.Empty)
                    {
                        _wLogger("Kann Entries.log nicht schreiben da der Pfad invalid ist!");
                    }
                    else
                    {
                        long length2 = attributes.Length;
                        sb3.Append(DateTime.Now.Date.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\t" + "= DirectoryWatcher =" + "\r\n");
                        sb3.Append(name + "\r\n");
                        sb3.Append("Changed to Type: " + types + "\r\n");
                        sb3.Append("Creation Time: " + creationTime.ToString() + "\r\n");
                        sb3.Append("Attributes: " + fattributes + "\r\n");
                        sb3.Append("Length: " + length2 + "\r\n");
                        sb3.Append("Owner: " + besitzer + "\r\n");
                        sb3.Append("Group: " + gruppe + "\r\n");

                        sb3.Append("\r\n");
                        File.AppendAllText(Path + @"\direntries.log", sb3.ToString());
                        sb3.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Logger.Path + @"\log.log", ex.Message + "\r\n");
            }
        }

        public void LogDirs (string pfad)
        {
            try
            {
                File.AppendAllText(Logger.Path + @"\dir.log", pfad);


            }
            catch ( Exception ex)
            {
                File.AppendAllText(Logger.Path + @"\log.log", ex.Message + "\r\n");
            }
           
        }

        public string GetPath ()
        {
            if ( File.Exists (Classes.Statics.appdata + @"\FileWatcher\save"))
            {
                int counter = 0;
                string line;
                StreamReader reader = new StreamReader(Classes.Statics.appdata + @"\FileWatcher\save");

                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    Path = line;
                    return Path;
                }
            }
            else
            {
                Path = "INVALID";
            }

            return Path;
            
        }

        public void ClearAllTheGarbage ()
        {
            try
            {
                sb.Clear();
                sb2.Clear();
            }
            catch ( Exception ex)
            {
                _wLogger("Can´t clear the Garbage: " + ex.Message);
            }
        }
    }
}
