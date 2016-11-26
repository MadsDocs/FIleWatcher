using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security;
using System.Windows;

namespace FileWatcher.Classes.FileSystem
{
    class Init
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public void _Init()
        {
            try
            {
                

                if (!Directory.Exists (appdata + @"\FileWatcher"))
                {
                    Logging.Logger log = new Logging.Logger();
                    log._wLogger("<ROOT> DIR exists!");
                }
                else
                {
                    Directory.CreateDirectory(appdata + @"\FileWatcher");
                    Directory.CreateDirectory(appdata + @"\FileWatcher\Logs");

                    Logging.Logger log = new Logging.Logger();
                    log._ilogger("<ROOT> DIR created!");


                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }

        }
    }
}
