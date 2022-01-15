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

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private static Logger log = new Logger();

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                string path = log.GetPath();
                FileInfo entries = new FileInfo(path + @"\entries.log");
                FileInfo errorlog = new FileInfo(path + @"\error.log");
                FileInfo fwlog = new FileInfo(path + @"\log.log");

                lbl_entriessize2.Content = entries.Length /1024;
                lbl_errorsize.Content = errorlog.Length /1024;
                lbl_logsize2.Content = fwlog.Length /1024;

            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
