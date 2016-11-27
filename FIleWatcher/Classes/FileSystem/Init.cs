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
    // 2) Sammeln von Daten über die Umgebung -> OS, Eigenes Verzeichnis (wird wichtig wenn das erstellen des Ordners unter %APPDATA% fehlschlägt!


    class Init
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string os;
        private static string platform;
        private static string servicepack;
        private static string owndir = Environment.CurrentDirectory;
        private static string execuser;
        private static string clr_version;
        private static string currdir;
        public static string eversion = Assembly.GetExecutingAssembly().GetName().Version.ToString();


        public static bool is_Home = false;
        private static bool is64 = false;

        private static Logger log = new Logger();

        public void _Init()
        {
            try
            {
                CreateHomeDir();
                Gatherer();

                if ( !File.Exists ( Options.appdata + @"\FileWatcher\save"))
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
            catch (Exception ex)
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
                

                Logger.sb.Clear();

                try
                {
                    if ( currdir == @"C:\Users\mario\documents\visual studio 2015\Projects\FileWatcher\FileWatcher\bin\Debug")
                    {
                        //DEBUG
                    }
                    else
                    {
                        RegistryKey key;
                        key = Registry.CurrentUser.CreateSubKey("FileWatcher");
                        key.SetValue("Dir", currdir);
                        key.SetValue("Version", eversion);
                        key.Close();
                    }




                }
                catch ( Exception ex )
                {
                    if ( is_Home )
                    {
                        log._wLogger(" Konnte CurrDir nicht setzen!");
                        log._eLogger(ex);
                    }
                    else
                    {

                    }
                }

                log._wLogger("OS: " + os);
                log._wLogger("Dir: " + owndir);
                log._wLogger("CLR_VERSION: " + clr_version);
                log._wLogger("GATHERER() fertig, versuche nun die neuerste Version zu bekommen!");

                CheckVersion();
                Logger.sb.Clear();
            }
            else
            {

            }
        }

        void CheckVersion ()
        {
            try
            {
                WebClient version = new WebClient();
                version.DownloadFile("https://themadbrainz.net/FileWatcher/version.txt", appdata + @"\FileWatcher\Patches\version.txt");


                string line;
                string dversion = "";
                int counter = 0;
                StreamReader reader = new StreamReader(appdata + @"\FileWatcher\Patches\version.txt");

                while ( (line = reader.ReadLine()) != null)
                {
                    line = dversion;
                }

                if ( eversion != dversion)
                {
                    log._wLogger("Neue Version verfügbar, benachrichtige nun den User!");
                    MessageBoxResult result =  MessageBox.Show("Neue Version verfügbar, soll nun die FileWatcher Website aufgerufen werden?", "Neue Version verfügbar!", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if ( result == MessageBoxResult.Yes)
                    {
                        ///TODO: Öffne Standart Browser

                        ///TODO: Cleanup Methodik einführen!
                        
                        if ( File.Exists ( appdata + @"\FileWatcher\Patches\version.txt"))
                            File.Delete(appdata + @"\FileWatcher\Patches\version.txt");


                    }
                    else
                    {
                        /// TODO: Tja dann wird der User das Programm von alleine runter laden müssen!
                    }
                }
                else
                {
                    log._wLogger("Überprüfung auf neue Version erfolgreich ausgeführt, es gibt keine neue Version!");
                }



            }
            catch ( Exception ex)
            {
                log._wLogger("Konnte nicht überprüfen ob es eine neue Version gibt! ");
                log._wLogger(ex.Message);
            }
        }

    }
}
