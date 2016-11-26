using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

namespace FileWatcher.Classes.Logging
{
    class Logger
    {
        /// <summary>
        /// Dies ist der Information Logger
        /// </summary>
        /// <param name="message"></param>
        public void _ilogger(string message)
        {

            if (Directory.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\"))
            {
                StreamWriter writer = new StreamWriter(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                writer.WriteLine(DateTime.Now + "\t|\t" + message);
                writer.Flush();
                writer.Close();
            }
            else
            {
                Directory.CreateDirectory(Classes.FileSystem.Init.appdata + @"\FileWatcher\");
                Directory.CreateDirectory(Classes.FileSystem.Init.appdata + @"\FileWatcher\");

                if (Directory.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs"))
                {
                    StreamWriter writer = new StreamWriter(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                    writer.WriteLine(DateTime.Now + "\t|\t" + message);
                    writer.Flush();
                    writer.Close();
                }
                else
                {
                    MessageBox.Show("Konnte <ROOT> DIR nicht erstellen");
                }

            }

        }

        /// <summary>
        /// Dies ist der Error Logger
        /// </summary>
        /// <param name="ex"></param>
        public void _eLogger(Exception ex)
        {
            if (Directory.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\"))
            {
                StreamWriter writer = new StreamWriter(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                writer.WriteLine(DateTime.Now + "\t|\t" + ex.Message);
                writer.WriteLine(DateTime.Now + "\t|\t" + ex.StackTrace);
                writer.Flush();
                writer.Close();
            }
            else
            {
                Directory.CreateDirectory(Classes.FileSystem.Init.appdata + @"\FileWatcher\");
                Directory.CreateDirectory(Classes.FileSystem.Init.appdata + @"\FileWatcher\");

                if (Directory.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs"))
                {
                    StreamWriter writer = new StreamWriter(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                    writer.WriteLine(DateTime.Now + "\t|\t" + ex.Message);
                    writer.WriteLine(DateTime.Now + "\t|\t" + ex.StackTrace);
                    writer.Flush();
                    writer.Close();
                }
                else
                {
                    MessageBox.Show("Konnte <ROOT> DIR nicht erstellen");
                }

            }

        }

        /// <summary>
        /// Dies ist der Warning Logger
        /// </summary>
        /// <param name="message"></param>
        public void _wLogger(string message)
        {
            if (Directory.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\"))
            {
                StreamWriter writer = new StreamWriter(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                writer.WriteLine(DateTime.Now + "\t|\t" + message);
                writer.Flush();
                writer.Close();
            }
            else
            {
                Directory.CreateDirectory(Classes.FileSystem.Init.appdata + @"\FileWatcher\");
                Directory.CreateDirectory(Classes.FileSystem.Init.appdata + @"\FileWatcher\");

                if (Directory.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs"))
                {
                    StreamWriter writer = new StreamWriter(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                    writer.WriteLine(DateTime.Now + "\t|\t" + message);
                    writer.Flush();
                    writer.Close();
                }
                else
                {
                    MessageBox.Show("Konnte <ROOT> DIR nicht erstellen");
                }

            }
        }
    }
}
