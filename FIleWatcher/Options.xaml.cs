using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.IO;
using FileWatcher.Classes;
using FileWatcher.Classes.Logging;
using System.Diagnostics;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public static string pathtolog = "";
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public Options()
        {
            InitializeComponent();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Stop();
            MainWindow.counter = 0;
            main.lbl_counter.Content = 0;
        }
        private void btn_root_dir_anzeigen_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", appdata + @"\FileWatcher");
        }

        private void btn_showSetPath_Click(object sender, RoutedEventArgs e)
        {
            SetPath path = new SetPath();
            path.Show();
        }

        private void TabItem_Initialized(object sender, EventArgs e)
        {
            try
            {
                lbl_explain.Content = " Zustand FileWatcher";

                if (Directory.Exists(Logger.path))
                {
                    FileInfo info = new FileInfo(Logger.path + @"\entries.log");
                    long length = info.Length / 1024;
                    lbl_entries.Content = "Entries Eigenschaften: \r\n"
                                         + " Größe: " + length + " kb \r\n"
                                         + "  Extension: " + info.Extension + "\r\n";
                }

                lbl_Path.Content = "Logger Pfad: " + Logger.path;
            }
            catch ( Exception ex )
            {
                lbl_entries.Content = "Entries.log wurde nicht gefunden!";
                File.AppendAllText(Logger.currentdir + @"\log.log", ex.Message);
            }
             
            

        }
    }
}
