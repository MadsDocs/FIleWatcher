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

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für ShowLog.xaml
    /// </summary>
    public partial class ShowLog : Window
    {
        private static int counter = 0;
        private static string line;
        static PerformanceCounter perfcounter;
        static Logger Log = new Logger();


        public ShowLog()
        {
            InitializeComponent();
        }

        private void rdb_showEntry_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                string path = Logger.path;
                int counter = 0;
                string line;

                if ( path == string.Empty)
                {
                    MessageBox.Show("Konnte keine entries.log finden!", "Datei wurde nicht gefunden!");
                }
                else
                {
                    StreamReader reader = new StreamReader(path + @"\entries.log");

                    lstbx_show.Items.Clear();
                    while ((line = reader.ReadLine()) != null)
                    {
                        counter++;
                        lstbx_show.Items.Add(" ( " + counter + " ) " + line);

                        lbl_counter.Content = "Counter ( Log ) : " + counter;
                    }
                    reader.Close();
                }


               
            }
            catch (Exception ex)
            {
                Log._eLogger(ex);
            }



        }

        private void rdb_showLog_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                int counter = 0;
                string line;

                if ( !File.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log"))
                {
                    MessageBox.Show("Log Datei nicht gefunden!");
                }
                else
                {
                    StreamReader reader = new StreamReader(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");
                    lstbx_show.Items.Clear();

                    while ((line = reader.ReadLine()) != null)
                    {
                        counter++;
                        lstbx_show.Items.Add(" ( " + counter + " ) " + line);
                    }
                    reader.Close();
                    lbl_counter.Content = "Counter ( Log ) : " + counter;
                }


                
            }
            catch (Exception ex)
            {
                Log._eLogger(ex);
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            string path = Logger.path;


            try
            {

                if (rdb_showEntry.IsChecked == true)
                {
                    int counter = 0;
                    string line;
                    
                    if ( path == string.Empty)
                    {
                        MessageBox.Show("entries.log wurde nicht gefunden!");
                    }
                    else
                    {

                        StreamReader reader = new StreamReader(path);

                        lstbx_show.Items.Clear();
                        while ((line = reader.ReadLine()) != null)
                        {
                            counter++;
                            lstbx_show.Items.Add(" ( " + counter + " ) " + line);

                            lbl_counter.Content = "Counter ( Log ) : " + counter;
                        }
                        reader.Close();

                    }

                }
                else if (rdb_showLog.IsChecked == true)
                {
                    int counter = 0;
                    string line;

                    if ( !File.Exists ( Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log"))
                    {
                        MessageBox.Show("Log Datei wurde nicht gefunden!");
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(Classes.FileSystem.Init.appdata + @"\FileWatcher\Logs\log.log");

                        lstbx_show.Items.Clear();
                        while ((line = reader.ReadLine()) != null)
                        {
                            counter++;
                            lstbx_show.Items.Add(" ( " + counter + " ) " + line);

                            lbl_counter.Content = "Counter ( Log ) : " + counter;
                        }
                        reader.Close();
                    }


                 
                }
            }
            catch (Exception ex)
            {
                Log._eLogger(ex);
            }

        }

        private void btn_showstats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(Classes.FileSystem.Init.appdata + @"\FileWatcher\Extensions\CPU Counter.exe"))
                {
                    lstbx_show.Items.Add("Kann keine Programm Statistiken anzeigen, da kein Programm vorhanden ist!");
                }
                else
                {
                    Process.Start(Classes.FileSystem.Init.appdata + @"\FileWatcher\Extensions\CPUCounter.exe");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
