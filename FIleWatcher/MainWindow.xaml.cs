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
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Reflection;

using FileWatcher.Classes.Logging;
using FileWatcher.Classes.FileSystem;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;
using System.Net;

namespace FileWatcher
{
    


    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int counter = 0;
        public static FileSystemWatcher fsw = new FileSystemWatcher();
        private static Logger log = new Logger();
        public string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public List<String> gDrives = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            log._wLogger("Programm started");
            windows.ResizeMode = ResizeMode.CanMinimize;
            Init inti = new Init();
            inti._Init();

            if ( File.Exists ( Options.appdata + @"\FileWatcher\save"))
            {
                string pfad = log.GetPath();
                Logger.path = pfad;
            }


        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = cmb_festplatten.SelectedItem.ToString();

                if (path == string.Empty)
                {
                    
                }
                else
                {
                    fsw.Path = path;
                    fsw.IncludeSubdirectories = true;
                    fsw.Filter = "*.*";
                    
                    
                    fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                    
                    if (fsw.EnableRaisingEvents == false)
                    {
                        try
                        {
                            fsw.EnableRaisingEvents = true;
                        }
                        catch (Exception ex)
                        {
                            Errorbehandlung.DisplayError(ex);
                        }
                    }


                    fsw.Changed += Fsw_Changed;
                    fsw.Created += Fsw_Created;
                    fsw.Renamed += Fsw_Renamed;
                    fsw.Deleted += Fsw_Deleted;
                    
                }
            }
            catch (Exception  ex)
            {
                MessageBox.Show(" Bitte eine Festplatte auswählen!", "Keine Festplatte wurde ausgewählt", MessageBoxButton.OK, MessageBoxImage.Error);
                log._eLogger(ex);
            }
        }

        private void Fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);
                if (info.Name == "entries.log" || info.Name == "log.log")
                {

                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Deleted, e.Name);
                    counter++;
                }
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
          

        }

        private void Fsw_Renamed(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);

                if (info.Name == "entries.log" || info.Name == "log.log")
                {
                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Renamed, e.FullPath);
                    counter++;
                }
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        



        }

        private void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);

                if (info.Name == "entries.log" || info.Name == "log.log" )
                {
                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Created, e.FullPath);
                    counter++;
                }
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        


        }

        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);

                if (info.Name == "entries.log" || info.Name == "log.log" )
                {
                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Changed, e.FullPath);
                    counter++;
                }

            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        }

        void DisplayFiles ( WatcherChangeTypes watcherTypes, string name, string oldname = null)
        {

            Logger logger = new Classes.Logging.Logger();

            try
            {
                if (watcherTypes == WatcherChangeTypes.Changed)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, watcherTypes.ToString()), watcherTypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogEntrys(name, DateTime.Now, watcherTypes); }));
                    //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { logger.EntriesWatcher(); }));



                }
                else if (watcherTypes == WatcherChangeTypes.Created)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, watcherTypes.ToString()), watcherTypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogEntrys(name, DateTime.Now, watcherTypes); }));
                    //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { logger.EntriesWatcher(); }));
                }
                else if (watcherTypes == WatcherChangeTypes.Deleted)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, watcherTypes.ToString()), watcherTypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogEntrys(name, DateTime.Now, watcherTypes); }));
                    //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { logger.EntriesWatcher(); }));
                }
                else if (watcherTypes == WatcherChangeTypes.Renamed)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, watcherTypes.ToString()), watcherTypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogEntrys(name, DateTime.Now, watcherTypes); }));
                    //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { logger.EntriesWatcher(); }));
                }
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        }

        public void AddtoList(string text, WatcherChangeTypes types)
        {
            switch (types)
            {
                case WatcherChangeTypes.Changed:
                    lstview_anzeige.Items.Add(text);
                    break;
                case WatcherChangeTypes.Created:
                    lstview_anzeige.Items.Add(text);
                    break;
                case WatcherChangeTypes.Deleted:
                    lstview_anzeige.Items.Add(text);
                    break;
                case WatcherChangeTypes.Renamed:
                    lstview_anzeige.Items.Add(text);
                    break;
                default:
                    lstview_anzeige.Items.Add(text);
                    break;
            }
        }

        void AutoScroll()
        {
            try
            {
                ListBoxAutomationPeer svAutomation = (ListBoxAutomationPeer)ScrollViewerAutomationPeer.CreatePeerForElement(lstview_anzeige);

                IScrollProvider scrollInterface = (IScrollProvider)svAutomation.GetPattern(PatternInterface.Scroll);
                System.Windows.Automation.ScrollAmount scrollVertical = System.Windows.Automation.ScrollAmount.LargeIncrement;
                System.Windows.Automation.ScrollAmount scrollHorizontal = System.Windows.Automation.ScrollAmount.NoAmount;
                //If the vertical scroller is not available, the operation cannot be performed, which will raise an exception. 
                if (scrollInterface.VerticallyScrollable)
                    scrollInterface.Scroll(scrollHorizontal, scrollVertical);
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }

        }

        private void btn_beenden_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        void DisplayCounter()
        {
            lbl_counter.Visibility = Visibility.Visible;
            lbl_counter.Content = "Dateizugriffe: " + counter;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                
                lbl_counter.Visibility = Visibility.Hidden;
                lbl_fwversion.Content = "Installierte Version: " + version;
                
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Options opt = new Options();
            opt.Show();
        }

        private void cmb_festplatten_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Alle verfügbaren Festplatten herausfinden, in eine Liste speichern
                DriveInfo[] drives = DriveInfo.GetDrives();
                

                foreach (var d in drives)
                {
                    if (d.IsReady)
                    {
                        cmb_festplatten.Items.Add(d);
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        }

        public void Stop()
        {
            fsw.EnableRaisingEvents = false;
        }

        private void lstview_anzeige_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = lstview_anzeige.SelectedItems[0];
                MessageBox.Show(item.ToString(), "Detailierte Informationen",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
            
        }

        private void ShowLogs()
        {
            ShowLog log = new ShowLog();
            log.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ShowLogs();
        }

        void SeekForUpdate ()
        {
            try
            {
                // Diese Methode updated den FileWatcher
                string line;
                string dversion = "";
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                WebClient client = new WebClient();
                client.DownloadFile("https://themadbrainz.net/FileWatcher/version.txt", Options.appdata + @"\FileWatcher\version.txt");

                StreamReader reader = new StreamReader(Options.appdata + @"\FileWatcher\save");

                while ((line = reader.ReadLine()) != null)
                {
                    line = dversion;
                }

                if (version != dversion)
                {
                    log._wLogger(" Update benötigt, starte nun den Updater! ");

                }
                else
                {
                    log._wLogger(" Kein Update benötigt! ");
                }
            }
            catch ( Exception ex)
            {
                log._wLogger(ex.ToString());
                MessageBox.Show("Konnte den FileWatcher nicht updaten! ");
            }
           


        }
    }
}