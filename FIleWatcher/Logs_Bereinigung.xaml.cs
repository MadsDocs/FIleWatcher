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

using Microsoft.Win32;
using wf = System.Windows.Forms;
using System.IO;

using System.Security.Cryptography;
using FileWatcher.Classes.FileSystem;
using FileWatcher.Classes.Logging;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für Logs_Bereinigung.xaml
    /// </summary>
    public partial class Logs_Bereinigung : Window
    {

        private Logger log = new Logger();
        private FIleOperations fo = new FIleOperations();

        public Logs_Bereinigung()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string loggerdir = Classes.FileSystem.FIleOperations.getLoggerDir();

                if ( rdb_backup.IsChecked == true)
                {
                    
                    //MessageBox.Show("CLICK!");
                    var folderbrowser = new wf.FolderBrowserDialog();
                    folderbrowser.ShowDialog();
                    var pfad = folderbrowser.SelectedPath;
                    bool isEmpty = fo.isDirEmpty(pfad);

                    if ( pfad == string.Empty)
                    {
                        MessageBox.Show(" Bitte einen Ordner auswählen!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if ( !isEmpty )
                    {
                        MessageBox.Show("Ziel Ordner ist nicht leer!", "Ordner ist nicht leer!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        foreach (string files in Directory.EnumerateFiles(loggerdir))
                        {
                            var hash = fo.Returnmd5Hash(files);
                            log._wLogger("HASH ( " + files + ") is " + hash);

                            FileInfo fi = new FileInfo(files);
                            string name = fi.Name;
                            string ext = fi.Extension;
                            File.Copy(files, pfad + "\\" + name);
                            File.Delete(files);
                        }

                        MessageBox.Show(" Backup der Dateien ist abgeschlossen! \tSie finden diese nun unter: " + pfad, "Backupvorgang abgeschlossen!", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                else
                {
                    foreach (string files in Directory.EnumerateDirectories ( loggerdir))
                    {
                        File.Delete(files);
                    }
                }

            }
            catch ( Exception ex)
            {

            }
        }
    }
}
