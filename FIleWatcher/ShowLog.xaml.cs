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
using System.ComponentModel;

using System.IO;
using System.Diagnostics;

using FileWatcher.Classes.Logging;
using FileWatcher.Classes;
using System.Windows.Threading;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für ShowLog.xaml
    /// </summary>
    public partial class ShowLog : Window
    {

        static Logger Log = new Logger();


        public ShowLog()
        {
            InitializeComponent();
        }

        private void rdb_showEntry_Checked(object sender, RoutedEventArgs e)
        {
            BackgroundWorker testWorker = new BackgroundWorker();
            DoWorkEventArgs dow = null;


            try
            {

                string path = Logger.Path;
                int counter = 0;
                string line;

                if ( path == string.Empty)
                {
                    MessageBox.Show(" Konnte entries.log nicht finden!", "Datei wurde nicht gefunden", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    using (StreamReader reader = new StreamReader(path + @"\entries.log"))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if ( testWorker.CancellationPending)
                            {
                                dow.Cancel = true;

                            }
                            else
                            {
                                counter++;
                                lstbx_show.Items.Add(line);
                                lbl_counter.Content = "Counter ( Log ): " + counter;
                            }
                        }
                    }

                    stopwatch.Stop();
                    MessageBox.Show("Elapsed Time: " + stopwatch.Elapsed);







                }
            }
            catch (Exception ex)
            {
                Log.ExLogger(ex);
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void rdb_showLog_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                int counter = 0;
                string line;

                if ( !File.Exists(Statics.appdata + @"\FileWatcher\Logs\log.log"))
                {
                    MessageBox.Show("Log Datei nicht gefunden!");
                }
                else
                {
                    StreamReader reader = new StreamReader(Statics.appdata + @"\FileWatcher\Logs\log.log");
                    lstbx_show.Items.Clear();

                    while ((line = reader.ReadLine()) != null)
                    {
                        counter++;
                        lstbx_show.Items.Add(line);
                    }
                    reader.Close();
                    lbl_counter.Content = "Counter ( Log ) : " + counter;
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

                    if ( !File.Exists (Statics.appdata + @"\FileWatcher\Logs\log.log"))
                    {
                        MessageBox.Show("Log Datei wurde nicht gefunden!");
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(Statics.appdata + @"\FileWatcher\Logs\log.log");

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
                Log.ExLogger(ex);
            }

        }

        private void btn_showstats_Click(object sender, RoutedEventArgs e)
        {
            Stats stats = new Stats();
            stats.Show();
        }
    }
}
