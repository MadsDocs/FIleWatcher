using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security;
using System.Windows;

using FileWatcher.Classes.Logging;
using Microsoft.Win32;
using System.Reflection;
using System.Net;

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
        private bool isRegistrySet = false;


        public static bool is_Home = false;

        private static Logger log = new Logger();

        public string Fwversion { get => ReadVersion(); }

        public void _Init()
        {
            try
            {
                CreateHomeDir();
                Gatherer();

                if ( !File.Exists (Classes.Statics.appdata + @"\FileWatcher\save"))
                {
                    SetPath path = new SetPath();
                    path.ShowDialog();
                }
                else
                {
                        log._wLogger("Logging aktiv... ");  
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

                is_Home = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Konnte kein Verzeichnis für den Filewatcher anlegen, starten Sie bitte das Programm als Administrator");
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
                            RegistryKey key;
                            key = Registry.CurrentUser.CreateSubKey("FileWatcher");
                            key.SetValue("Dir", currdir);
                            key.SetValue("Version", fwversion);
                            key.Close();

                            isRegistrySet = true;



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
            //BLA
            try
            {
                RegistryKey version;
                version = Registry.CurrentUser.OpenSubKey("FileWatcher");
                version.GetValue("Version");

                if (version.ToString() == string.Empty)
                {
                    //Tja, dann ist wohl noch nie was in die Registry geschrieben worden!
                    // Und wir werden wohl oder übel die Version über die Assembly auslesen müssen!
                    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                else
                {
                    string dVersion = version.GetValue("Version").ToString();
                    return dVersion;
                }
            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
                return null;
            }
         
        }


    }
}
