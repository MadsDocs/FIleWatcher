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
        public static StringBuilder sb = new StringBuilder();
        private static StringBuilder sb2 = new StringBuilder();
        public static string path = "";
        
        /// <summary>
        /// Dies ist der Error Logger
        /// </summary>
        /// <param name="ex"></param>
        public void _eLogger(Exception ex)
        {
            try
            {
                if (!Directory.Exists(FileSystem.Init.appdata + @"\FileWatcher"))
                {
                    sb.Append(DateTime.Now.ToLongDateString() + "\t" + "<ROOT DIR NOT CREATED... MAYBE INSUFFIENT PERMISSIONS?");
                    File.AppendAllText(currentdir + @"\log.log", sb.ToString());
                    sb.Clear();

                }
                else
                {
                    sb.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + ex.Message + "\r\n").ToString();
                    File.AppendAllText(FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log", sb.ToString());
                    sb.Clear();
                }
            }
            catch (Exception exe)
            {

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

                if (!Directory.Exists(FileSystem.Init.appdata + @"\FileWatcher"))
                {
                    sb.Append(DateTime.Now.ToLongDateString() + "\t" + "<ROOT DIR NOT CREATED... MAYBE INSUFFIENT PERMISSIONS?");
                    File.AppendAllText(currentdir + @"\log.log", sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + message + "\r\n").ToString();
                    File.AppendAllText(FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log", sb.ToString());
                    sb.Clear();
                }
            }
            catch (Exception ex)
            {

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



                string extension = attributes.Extension;

                if (extension == ".tmp")
                {
                    if ( path == "INVALID" || path == string.Empty)
                    {
                        _wLogger("Kann Entries.log nicht schreiben da der Pfad invalid ist!");
                    }
                    else
                    {
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + name + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Changed to Type: " + types + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Creation Time: " + creationTime.ToString() + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Attributes: " + fattributes + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Besitzer: " + besitzer + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Gruppe: " + gruppe + "\r\n");
                        sb2.Append("\r\n");
                        File.AppendAllText(path + @"\entries.log", sb2.ToString());
                        sb2.Clear();
                    }
                    

                }
                else
                {
                    if ( path == "INVALID" || path == string.Empty)
                    {
                        _wLogger("Kann Entries.log nicht schreiben da der Pfad invalid ist!");
                    }
                    else
                    {
                        long length2 = attributes.Length;
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + name + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Changed to Type: " + types + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Creation Time: " + creationTime.ToString() + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Attributes: " + fattributes + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Length: " + length2 + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Besitzer: " + besitzer + "\r\n");
                        sb2.Append(DateTime.Now.Date.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + "Gruppe: " + gruppe + "\r\n");
                        sb2.Append("\r\n");
                        File.AppendAllText(path + @"\entries.log", sb2.ToString());
                        sb2.Clear();
                    }

               

                }



            }
            catch (Exception ex)
            {
                ///TODO: Bessere Methode implementieren um dem User anzuzeigen das nicht ALLE Einträge mit geloggt werden!

            }
        }

        public string GetPath ()
        {
            if ( File.Exists ( Options.appdata + @"\FileWatcher\save"))
            {
                int counter = 0;
                string line;
                StreamReader reader = new StreamReader(Options.appdata + @"\FileWatcher\save");

                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    path = line;
                    return path;
                }
            }
            else
            {
                path = "INVALID";
            }

            return path;
            
        }
    }
}
