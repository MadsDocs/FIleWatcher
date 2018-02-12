using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Diagnostics;

namespace FileWatcher.Classes.FileSystem
{
    class Instanz
    {
        public static void StartNewInstanz ()
        {
         /*
            // Diese Methode startet eine neue Instanz vom FileWatcher!
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("FileWatcher");
                string dir = (string)rk.GetValue("Dir", "");
                string start = dir + @"\FileWatcher";

                if ( dir == string.Empty)
                {
                    MainWindow window = new MainWindow();
                    window.lbl_Messages.Content = " Neue Instanz konnte nicht erstellt werden!";
                }
                else
                {
                    Process.Start(start);
                }


                
            }
            catch ( Exception ex)
            {

            }

            */

        }


        public static void InstanzManager ()
        {
            int id = 0;




        }


    }
}
