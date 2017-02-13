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
        public static string eversion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static bool is_Home = false;

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
                        //DEBUG
                        log._wLogger("DEBUGGING AKTIV!");
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
                log._wLogger("GATHERER() fertig, versuche nun die neuerste Version zu bekommen!");
            }
            else
            {

            }
        }


    }
}
