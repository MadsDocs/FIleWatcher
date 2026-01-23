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
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Windows.Documents;

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
        private static readonly object errorlock = new object();


        //public static string Path { get => path; set => path = value; }


        /// <summary>
        /// Dies ist der Error Logger
        /// </summary>
        /// <param name="ex"></param>
        public async void _eLogger(Exception ex)
        {
            try
            {
                await Task.Run(async () => {

                    if (!Directory.Exists(Statics.appdata + @"\FileWatcher"))
                    {
                        MessageBox.Show("There is no FileWatcher Directory, please restart FileWatcher, or create a new Direcotry under %appdata% named FileWatcher!", "No FileWatcher Directory", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        sb.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "| ERROR |" + "\t" + ex.Message + "\r\n").ToString();
                        //File.AppendAllText(Statics.appdata + @"\FileWatcher\Logs\log.log", sb.ToString());

                        await Task.Run(() =>
                        {
                            using (StreamWriter writer = new StreamWriter(Statics.appdata + @"FileWatcher\Logs\log.log", true))
                            {
                                writer.WriteLineAsync(sb.ToString());
                            }
                        });
                        sb.Clear();
                    }

                });
                
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        
        public async void ExLogger ( Exception ex)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (!Directory.Exists(Statics.appdata + @"\FileWatcher"))
                    {
                        MessageBox.Show("There is no FileWatcher Directory, please restart the FileWatcher, or create a new Direcotry under %appdata% named FileWatcher!", "No FileWatcher Directory", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        string fwpath = GetPath();

                        lock (errorlock)
                        {
                            error.Append(DateTime.Now.ToLongDateString() + "\t" + ex.Message + "\r\n");
                            error.Append(DateTime.Now.ToLongDateString() + "\t" + ex.StackTrace + "\r\n");

                            File.AppendAllText(fwpath + @"\error.log", error.ToString() + "\r\n");

                            // Optional: synchron schreiben, da wir im Lock sind
                            using (StreamWriter writer = new StreamWriter(fwpath + @"\error.log", true))
                            {
                                writer.WriteLine(error.ToString());
                            }
                            error.Clear();
                        }
                    }
                });

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
        public async void _wLogger(string message)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (!Directory.Exists(Statics.appdata + @"\FileWatcher"))
                    {
                        MessageBox.Show("There is no FileWatcher Directory, please restart the FileWatcher, or create a new Direcotry under %appdata% named FileWatcher!", "No FileWatcher Directory", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        lock (sb)
                        {
                            sb.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + message + "\r\n");
                            using (StreamWriter writer = new StreamWriter(Logger.Path + @"\log.log", true, Encoding.UTF8))
                            {
                                writer.WriteLine(sb.ToString());
                            }
                            sb.Clear();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //File.AppendAllText(Logger.Path + @"\log.log", ex.Message + "\r\n");
                /*using (StreamWriter writer = new StreamWriter(Logger.Path + @"\log.log", true))
                {
                    await writer.WriteLineAsync(ex.Message);
                    
                }*/
                
                
            }
        }
        public async void LogEntrys (string name, DateTime time, WatcherChangeTypes types)
        {
            try
            {
                await Task.Run(async () => {
                    FileInfo attributes = new FileInfo(name);
                    DateTime creationTime = attributes.CreationTimeUtc;
                    string fattributes = attributes.Attributes.ToString();

                    string pattern = @"([a-zA-Z]:\\[^*|""<>?\n]*)|(\\\\.*?\\.*?\\.*?\\[^*|""<>?\n]*)";
                    Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                    MatchCollection matches = rex.Matches(name);
                    foreach (Match match in matches)
                    {
                        string path = match.Value;
                        //replace the - with a space
                        path = path.Replace("-", " ");
                        path = path.Replace("Changed", " ");
                        path = path.Replace("Created", " ");
                        path = path.Replace("Deleted", " ");
                        path = path.Replace("Renamed", " ");
                        path = path.Replace("->", " ");
                        path = path.Replace("}", "");
                        FileInfo finfo = new FileInfo(path);

                        //Get the Owner of the File
                        if (File.Exists(path.ToString()))
                        {
                            FileInfo finfo2 = new FileInfo(path.ToString());
                            FileSecurity fsec = finfo2.GetAccessControl();
                            IdentityReference idOwner = fsec.GetOwner(typeof(NTAccount));
                            IdentityReference idGroup = fsec.GetGroup(typeof(NTAccount));

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
                                        sb2.Append(DateTime.Now.Date.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n");
                                        sb2.Append(name + "\r\n");
                                        sb2.Append("Changed to Type: " + types + "\r\n");
                                        sb2.Append("Creation Time: " + creationTime.ToString() + "\r\n");
                                        sb2.Append("Attributes: " + fattributes + "\r\n");
                                        sb2.Append("Owner: " + idOwner + "\r\n");
                                        sb2.Append("Group: " + idGroup + "\r\n");
                                        sb2.Append("\r\n");
                                        



                                        using (StreamWriter writer = new StreamWriter(Path + @"\entries.log", true, Encoding.UTF8))
                                        {
                                            await writer.WriteLineAsync(sb2.ToString());
                                        }
                                            
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
                                        sb2.Append("Owner: " + idOwner + "\r\n");
                                        sb2.Append("Group: " + idGroup + "\r\n");
                                        sb2.Append("\r\n");
                                        

                                        using (StreamWriter writer = new StreamWriter(Path + @"\entries.log", true, Encoding.UTF8))
                                        {
                                            await writer.WriteLineAsync(sb2.ToString());
                                        }
                                        sb2.Clear();
                                    }
                                }
                            }
                            else
                            {
                                sb2.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "== Datei wurde nicht mitgeloggt! " + attributes.Name + " == ");
                            }

                        }
                        else
                        {
                            
                        }
                    }
                });
                
            }
            catch (Exception ex)
            {
                //File.AppendAllText(ex.Message + "\r\n");
                /*using (StreamWriter writer = new StreamWriter(Logger.Path + @"\log.log", true))
                {
                    await writer.WriteLineAsync(ex.Message);
                }*/
            }

        }

        public async void LogDirEntrys(string name, DateTime time, WatcherChangeTypes types)
        {
            try
            {
                await Task.Run(async () => {
                    FileInfo attributes = new FileInfo(name);
                    DateTime creationTime = attributes.CreationTimeUtc;

                    string fattributes = attributes.Attributes.ToString();

                    var besitzer = new FileInfo(name).GetAccessControl().GetOwner(typeof(NTAccount));
                    var gruppe = new FileInfo(name).GetAccessControl().GetGroup(typeof(NTAccount));



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
                            //File.AppendAllText(Path + @"\direntries.log", sb3.ToString());

                            using (StreamWriter writer = new StreamWriter(Path + @"\direntries.log", true))
                            {
                                await writer.WriteLineAsync(sb3.ToString());
                            }
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
                            //File.AppendAllText(Path + @"\direntries.log", sb3.ToString());

                            using (StreamWriter writer = new StreamWriter(Path + @"\direntries.log", true))
                            {
                                await writer.WriteLineAsync(sb3.ToString());
                            }
                            sb3.Clear();

                            sb3.Clear();
                        }
                    }
                });
                
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
