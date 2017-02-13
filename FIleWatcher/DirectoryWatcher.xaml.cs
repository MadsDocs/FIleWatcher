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
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using FileWatcher.Classes.Logging;


namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für DirectoryWatcher.xaml
    /// </summary>
    public partial class DirectoryWatcher : Window
    {
        public DirectoryWatcher()
        {
            InitializeComponent();
        }

        private static FileSystemWatcher fsw = new FileSystemWatcher();
        private static Logger log = new Logger();

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            string WatchDir = txtbx_Dir.Text;

            if ( WatchDir == string.Empty)
            {
                lbl_fehler.Content = " Fehlendes Verzeichnis!";
            }
            else
            {
                
                fsw.Path = WatchDir;
                fsw.Filter = "*.*";

                fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                fsw.IncludeSubdirectories = true;
                
                if ( fsw.EnableRaisingEvents == false)
                {
                    try
                    {
                        fsw.EnableRaisingEvents = true;
                    }
                    catch ( Exception ex)
                    {
                        lbl_fehler.Content = ex.Message;
                    }
                }

                fsw.Changed += Fsw_Changed;
                fsw.Created += Fsw_Created;
                fsw.Deleted += Fsw_Deleted;
                fsw.Renamed += Fsw_Renamed;
                lbl_fehler.Content = "Überwachung gestartet";

            }
        }

        private void Fsw_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);

                if (info.Name == "entries.log" || info.Name == "log.log")
                {

                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Deleted, e.FullPath);
                }
            }
            catch ( Exception ex)
            {
                lbl_fehler.Content = ex.Message;
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
                    DisplayFiles(WatcherChangeTypes.Deleted, e.FullPath);
                }
            }
            catch (Exception ex)
            {
                lbl_fehler.Content = ex.Message;
            }
        }

        private void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);

                if (info.Name == "entries.log" || info.Name == "log.log")
                {

                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Deleted, e.FullPath);
                }
            }
            catch (Exception ex)
            {
                lbl_fehler.Content = ex.Message;
            }
        }

        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo info = new FileInfo(e.Name);

                if (info.Name == "entries.log" || info.Name == "log.log")
                {

                }
                else
                {
                    DisplayFiles(WatcherChangeTypes.Deleted, e.FullPath);
                }
            }
            catch (Exception ex)
            {
                lbl_fehler.Content = ex.Message;
            }
        }


        void DisplayFiles ( WatcherChangeTypes wtypes, string name, string oldname = null)
        {
            try
            {
                if (wtypes == WatcherChangeTypes.Changed)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, wtypes.ToString()), wtypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogDirEntrys(name, DateTime.Now, wtypes); }));
                }
                else if (wtypes == WatcherChangeTypes.Created)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, wtypes.ToString()), wtypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogDirEntrys(name, DateTime.Now, wtypes); }));
                }
                else if (wtypes == WatcherChangeTypes.Deleted)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, wtypes.ToString()), wtypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogDirEntrys(name, DateTime.Now, wtypes); }));
                }
                else if (wtypes == WatcherChangeTypes.Renamed)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, name, wtypes.ToString()), wtypes); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() => { log.LogDirEntrys(name, DateTime.Now, wtypes); }));
                }
            }
            catch (Exception ex)
            {
                lbl_fehler.Content = ex.Message;
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
                lbl_fehler.Content = ex.Message;
            }

        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fsw.EnableRaisingEvents = false;
                lbl_fehler.Content = "Überwachung beendet";
            }
            catch ( Exception ex)
            {
                lbl_fehler.Content = ex.Message;
            }
            

        }
    }
}
