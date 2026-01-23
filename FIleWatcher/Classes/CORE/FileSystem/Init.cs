using FileWatcher.Classes.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileWatcher.Classes.FileSystem
{
    //Folgende Sachen sollen Initalisiert werden wenn der FileWatcher gestartet wird:
    // 1) Dateisystem -> Ordnerstruktur unter %APPDATA%\FileWatcher soll angelegt werden
    //                -> Eventuelle Ordner (Patches, Temp, Logs, Rollbacks) sollen angelegt werden


    class Init
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string os;
        private static string currdir;
        public static bool is_entriesenabled = false;
        public static bool is_statsenabled = false;
        
        public static bool is_Home = false;

        private static Logger log = new Logger();

        public string Fwversion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public void _Init()
        {
            try
            {
                CreateHomeDir();
                Gatherer();

                if (!File.Exists(Classes.Statics.appdata + @"\FileWatcher\save"))
                {
                    SetPath path = new SetPath();
                    path.ShowDialog();
                }
                else
                {
                    log._wLogger("Logging aktiv... ");
                }

                if (File.Exists(Classes.Statics.appdata + @"\FileWatcher\config\App.config"))
                {
                    //Read the Settings
                    string toggleentries = FIleOperations.getAppSettings("showentries");
                    toggleentries = toggleentries.ToLower();
                    if (toggleentries == "true")
                    {
                        is_entriesenabled = true;
                    }
                    else
                    {
                        is_entriesenabled = false;
                    }

                    string togglestats = FIleOperations.getAppSettings("showstats");
                    togglestats = togglestats.ToLower();
                    if (togglestats == "true")
                    {
                        is_statsenabled = true;
                    }
                    else
                    {
                        is_statsenabled = false;
                    }

                    string toggleClearLogFiles = FIleOperations.getAppSettings("clearlogfiles");
                    toggleClearLogFiles = toggleClearLogFiles.ToLower();

                    if (toggleClearLogFiles == "true")
                    {
                        //Clear the Logfiles
                        FIleOperations.ClearLogFiles();
                    }
                    else
                    {
                        //Do nothing
                    }


                }
                else if (File.Exists(@"D:\FileWatcher_BITBUCKET\FileWatcher\App.config"))
                {
                    //Read the Settings
                    string toggleentries = FIleOperations.getAppSettings("showentries");
                    toggleentries = toggleentries.ToLower();
                    if (toggleentries == "True")
                    {
                        is_entriesenabled = true;
                    }
                    else
                    {
                        is_entriesenabled = false;
                    }

                    string togglestats = FIleOperations.getAppSettings("showstats");
                    togglestats = togglestats.ToLower();
                    if (togglestats == "True")
                    {
                        is_statsenabled = true;
                    }
                    else
                    {
                        is_statsenabled = false;
                    }
                }
                else
                {
                    is_entriesenabled = false;
                    is_statsenabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }

        }

        private void CreateHomeDir()
        {
            try
            {
                Directory.CreateDirectory(appdata + @"\FileWatcher");
                Directory.CreateDirectory(appdata + @"\FileWatcher\Logs");
                Directory.CreateDirectory(appdata + @"\FileWatcher\Patches");
                Directory.CreateDirectory(appdata + @"\FileWatcher\Rollback");
                Directory.CreateDirectory(appdata + @"\FileWatcher\Extensions");
                Directory.CreateDirectory(appdata + @"\FileWatcher\config");

                is_Home = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Could not create the HomeDirectory for the filewatcher! Please start the FileWatcher as Administrator!");
                is_Home = false;

            }
        }

        private void Gatherer()
        {
            if (is_Home)
            {
                log._wLogger("<ROOT DIR CREATED>");
                os = Environment.OSVersion.Version.ToString();
                currdir = Environment.CurrentDirectory;

                try
                {
                    if ( currdir == @"C:\Users\mario\documents\visual studio 2015\Projects\FileWatcher\FileWatcher\bin\Debug")
                    {

                    }
                    else
                    {
                        string fwversion = ReadVersion();

                        //MessageBox.Show(fwversion);

                        if (string.IsNullOrEmpty(fwversion))
                        {
                            // Display a MessageBox... or do something with it, i do not really care :)
                        }
                        else
                        {


                        }


                    }
                }
                catch ( Exception ex )
                {
                    if ( is_Home )
                    {
                        log.ExLogger(ex);
                    }
                    else
                    {

                    }
                }

                log._wLogger("OS: " + os);

                ///TODO Updater schreiben!
            }
            else
            {

            }
        }

        public string ReadVersion ()
        {
            try
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();

            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
                return null;
            }
         
        }

        //function to make sure that the filewatcher is ready to run
        //this would include the following:
        //checking if the filewatcher home under %appdata% exists
        //checking if the firewall exception for data transfer exists
        //quick check if the filewatcher server could be reached (only activate this when datatransfer is enabled in the settings)
        public string startupChecker()
        {
            return "";
        }


    }
}
