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
using System.Diagnostics;

using FileWatcher.Classes.Logging;
using FileWatcher.Classes;
using FileWatcher.Classes.FileSystem;
using System.Threading;
using System.Windows.Threading;
using System.Security.Principal;
using System.Security.AccessControl;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für ShowLog.xaml
    /// </summary>
    public partial class ShowLog : Window
    {

        static Logger Log = new Logger();
        static FIleOperations operations = new FIleOperations();


        public ShowLog()
        {
            InitializeComponent();
            string fwpath = Log.GetPath();
        }

        private void rdb_showEntry_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = Logger.Path;
                int counter = 0;
                string line;

                if ( path == string.Empty)
                {
                    MessageBox.Show("Konnte keine entries.log finden!", "Datei wurde nicht gefunden!");
                }
                else
                {
                    StreamReader reader = new StreamReader(path + @"\entries.log");
                    counter++;
                    while ((line = reader.ReadLine()) != null)
                    {
                        
                        lstbx_show.Items.Add(line);

                        lbl_counter.Content = "Counter ( Log ) : " + counter;
                    }
                    reader.Dispose();
                    reader.Close();


                }
            }
            catch (Exception ex)
            {
                Log.ExLogger(ex);
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            string path = Logger.Path;
            
            try
            {

                bool isModified = operations.checkLastWriteFile(path + @"\entries.log");


                if (isModified)
                {

                    if (File.Exists((path + @"\entries.log.tmp")))
                    {
                        File.Delete(path + @"\entries.log.tmp");
                        File.Copy(path + @"\entries.log", path + @"\entries.log.tmp");

                    }
                    else
                    {
                        File.Copy(path + @"\entries.log", path + @"\entries.log.tmp");
                    }

                    lstbx_show.Items.Refresh();
                    int counter = 0;
                    string line;

                    if (path == string.Empty)
                    {
                        MessageBox.Show("entries.log wurde nicht gefunden!");
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(path + @"\entries.log.tmp");
                        lstbx_show.Items.Clear();
                        while ((line = reader.ReadLine()) != null)
                        {
                            counter++;
                            lstbx_show.Items.Add(line);
                            lbl_counter.Content = "Counter ( Log ) : " + counter;
                        }
                        reader.Dispose();
                        reader.Close();
                        File.Delete(path + @"\entries.log.tmp");
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Log.ExLogger(ex);
            }

        }

        private void btn_showstats_Click(object sender, RoutedEventArgs e)
        {
            Stats stats = new Stats();
            stats.Show();
        }

        private void lstbx_show_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = lstbx_show.SelectedItems[0];
                //MessageBox.Show(item.ToString(), "Detailierte Informationen",MessageBoxButton.OK, MessageBoxImage.Information);

                //MessageBox.Show(item.ToString());
                FileInfo finfo = new FileInfo(item.ToString());

                var besitzer = File.GetAccessControl(item.ToString()).GetOwner(typeof(NTAccount));
                var gruppe = File.GetAccessControl(item.ToString()).GetGroup(typeof(NTAccount));
                
                MessageBox.Show("Name: " + finfo.Name + "\n\r" + "Extension: " + finfo.Extension + "\n\r" + "Permissions: " + "\n\r" + "Owner: " + besitzer + "(" + gruppe + ")" + "\n\r" + "Size: " + finfo.Length + "\n\r" + "Path: " + item.ToString(), "FileInformation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log._eLogger(ex);
            }
        }
    }
}
