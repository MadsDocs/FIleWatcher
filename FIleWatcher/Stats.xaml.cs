using FileWatcher.Classes.FileSystem;
using FileWatcher.Classes.Logging;
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

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        public Stats()
        {
            InitializeComponent();
            FIleOperations ops = new FIleOperations();
            Logger log = new Logger();
            string path = log.GetPath();
            long filesize_entries = ops.getFileSize(path + @"\entries.log");
            long filesize_log = ops.getFileSize(path + @"\log.log");
            long filesize_error = ops.getFileSize(path + @"\error.log");

            lbl_currentlogsize.Content = "Entries Filesize: " + filesize_entries + "\n"+ 
                                         "Log Filesize: " + filesize_log + "\n"+ 
                                         "Error Filesize: " + filesize_error + "\n";


        }
    }
}
