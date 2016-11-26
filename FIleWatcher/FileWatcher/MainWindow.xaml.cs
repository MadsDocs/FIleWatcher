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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;


namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
               try
                {
                    FileSystemWatcher fsw = new FileSystemWatcher();
                    fsw.Path = @"C:\";
                    fsw.IncludeSubdirectories = true;
                    fsw.Filter = "*.*";
                    fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                    fsw.EnableRaisingEvents = true;

                  //  lstbx_anzeige.SelectedIndex = lstbx_anzeige.Items.Count - 1;
                  //  lstbx_anzeige.ScrollIntoView(lstbx_anzeige.SelectedIndex);



                    fsw.Changed += Fsw_Changed;
                    fsw.Created += Fsw_Created;
                    fsw.Renamed += Fsw_Renamed;


                }
                catch (Exception  ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message);
                }
        }

        private void Fsw_Renamed(object sender, FileSystemEventArgs e)
        {
            DisplayFiles(WatcherChangeTypes.Renamed, e.Name);
        }

        private void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            DisplayFiles(WatcherChangeTypes.Created, e.Name);
        }

        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            DisplayFiles(WatcherChangeTypes.Changed, e.Name);
        }

        void DisplayFiles ( WatcherChangeTypes watcherTypes, string name, string oldname = null)
        {

            Classes.Logging.Logger logger = new Classes.Logging.Logger();

           if (watcherTypes == WatcherChangeTypes.Changed)
           {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AddtoList(string.Format("{0} -> {1} to {2} - {3}", watcherTypes.ToString(), oldname, name, DateTime.Now)); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AutoScroll(); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { logger._ilogger("Displaying ('CHANGED')" ); }));
            }
           else if (watcherTypes == WatcherChangeTypes.Created)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AddtoList(string.Format("{0} -> {1} to {2} - {3}", watcherTypes.ToString(), oldname, name, DateTime.Now)); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AutoScroll(); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { logger._ilogger("Displaying ('CREATED')"); }));
            }
           else if ( watcherTypes == WatcherChangeTypes.Deleted)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AddtoList(string.Format("{0} -> {1} to {2} - {3}", watcherTypes.ToString(), oldname, name, DateTime.Now)); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AutoScroll(); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { logger._ilogger("Displaying ('DELETED')"); }));
            }
           else if ( watcherTypes == WatcherChangeTypes.Renamed)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AddtoList(string.Format("{0} -> {1} to {2} - {3}", watcherTypes.ToString(), oldname, name, DateTime.Now)); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { AutoScroll(); }));
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { logger._ilogger("Displaying ('RENAMED')"); }));
            }
        }

        public void AddtoList(string text)
        {
            this.lstbx_anzeige.Items.Add(text);
            int sIndex = lstbx_anzeige.SelectedIndex = lstbx_anzeige.Items.Count - 1;
            lstbx_anzeige.SelectedIndex = sIndex;
            lstbx_anzeige.Items.Refresh();
        }

        void AutoScroll()
        {
            //Autoscroll
            //lstbx_anzeige.SelectedIndex = lstbx_anzeige.Items.Count - 1;
            //lstbx_anzeige.ScrollIntoView(lstbx_anzeige.SelectedIndex);
            
        }

        private void btn_beenden_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }
    }
}