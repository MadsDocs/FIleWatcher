﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using System.IO;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

using FileWatcher.Classes.Logging;
using FileWatcher.Classes.FileSystem;
using System.Windows.Threading;
using System.Diagnostics;
using System.Security.Principal;

using System.Text.RegularExpressions;
using System.Security.AccessControl;

using System.Threading.Tasks;
using System.Web.UI.WebControls.Expressions;
using System.Web.UI.WebControls;
using FileWatcher.Properties;
using FileWatcher.Classes.CORE.FileSystem;



namespace FileWatcher
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int counter = 0;
        public static FileSystemWatcher fsw = new FileSystemWatcher();
        private Errorbehandlung behandlung = new Errorbehandlung();
        //private readonly Classes.Statics st;
        private Logger logger = new Logger();
        private static Logger log = new Logger();
        private static FIleOperations fo = new FIleOperations();
        private static string LoggerDirPath;
        private static ShowLog logs = new ShowLog();
        
        public static string pfad = log.GetPath();
        private int pfcounter = 0;

        public MainWindow()
        {
            InitializeComponent();
            log._wLogger("Programm started");
            windows.ResizeMode = ResizeMode.CanMinimize;
            Init inti = new Init();
            inti._Init();
            lbl_fwversion.Content = "Installierte Version: " + inti.Fwversion;
            lbl_Messages.Visibility = Visibility.Hidden;

            try
            {
                lbl_ordner.Content = "LogOrdner: " + FIleOperations.getLoggerDir();
                LoggerDirPath = FIleOperations.getLoggerDir();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(@"Konnte lbl_ordner_content nicht binden!" + "\t" + ex.Message, "getLoggerDir ist null", MessageBoxButton.OK, MessageBoxImage.Error);

                Environment.Exit(-1);
            }
             

            if ( File.Exists (Classes.Statics.appdata + @"\FileWatcher\save"))
            {
                SetPathMenu.IsEnabled = false;
            }

            

            //Enable or disable the Stats / entries Window:
            if ( Init.is_entriesenabled == true )
            {
                
                logs.Show();
                logs.rdb_showEntry.IsChecked = true;
            }
            else
            {
                //We will show no logs...
            }

            if ( Init.is_statsenabled == true)
            {
                logs.btn_showstats.IsEnabled = true;
            }
            else
            {
                logs.btn_showstats.IsEnabled = false;
            }
        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if the Logger Dir is empty!
               
                if (LoggerDirPath == string.Empty)
                {
                    MessageBox.Show("Konnte keinen Log Ordner finden! Bitte einmal SetPath ausführen!", "Logger Dir nicht gefunden!", MessageBoxButton.OK, MessageBoxImage.Error);
                  
                }
                else
                {

                    string path = cmb_festplatten.SelectedItem.ToString();
                    string trimmedpath = path.Substring(0,3);

                    lbl_Messages.Visibility = Visibility.Visible;
                    lbl_Messages.Content = "Überwachung gestartet!";

                    if (path == string.Empty)
                    {
                        logger._wLogger("Pfad is empty, skipping..");
                    }
                    else
                    {
                        fsw.Path = trimmedpath;
                        fsw.IncludeSubdirectories = true;
                        fsw.Filter = "*.*";


                        /*fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                        if (fsw.EnableRaisingEvents == false)
                        {
                            try
                            {
                                fsw.EnableRaisingEvents = true;
                            }
                            catch (Exception ex)
                            {
                                behandlung.DisplayError(ex);
                            }
                        }


                        fsw.Changed += Fsw_Changed;
                        fsw.Created += Fsw_Created;
                        fsw.Renamed += Fsw_Renamed;
                        fsw.Deleted += Fsw_Deleted;*/

                        InitializeFileSystemWatcher();

                    }
                }

                
            }
            catch (Exception  ex)
            {
                MessageBox.Show(" Bitte eine Festplatte auswählen!", "Keine Festplatte wurde ausgewählt", MessageBoxButton.OK, MessageBoxImage.Error);
                log.ExLogger(ex);


            }
        }
        /*
        private async void Fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Run(() => {

                    FileInfo info = new FileInfo(e.Name);
                    string owner = fo.GetOwnerofFile(e.Name);
                    string windir = Environment.GetEnvironmentVariable("windir");
                    logger._wLogger("Found windir under" + windir);

                    if (info.Name == "entries.log" || info.Name == "log.log" || info.Name == "error.log")
                    {

                    }
                    else if (info.Extension.Equals(".pf", StringComparison.OrdinalIgnoreCase))
                    {
                        pfcounter++;
                        logger._wLogger("Skipping .pf Files...");
                        logger._wLogger("Current PF Counter: " + pfcounter);
                    }
                    else if (info.FullName.StartsWith(windir, StringComparison.OrdinalIgnoreCase))
                    {
                        logger._wLogger("Skipping files in %windir% directory...");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(owner))
                        {
                            logger._wLogger("Datei ohne Besitzer gefunden, wird nicht geloggt...");
                        }
                        else
                        {
                            DisplayFiles(WatcherChangeTypes.Deleted, e.Name, owner);
                            counter++;
                        }
                    }
                    //Original Code

                });
                
            }
            catch (Exception ex)
            {
                log.ExLogger(ex);
            }
          

        }

        /*private async void Fsw_Renamed(object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Run(() => {
                    FileInfo info = new FileInfo(e.Name);
                    string owner = fo.GetOwnerofFile(e.Name);
                    string windir = Environment.GetEnvironmentVariable("windir");
                    logger._wLogger("Found windir under" + windir);

                    if (info.Name == "entries.log" || info.Name == "log.log" || info.Name == "error.log")
                    {

                    }
                    else if (info.Extension.Equals(".pf", StringComparison.OrdinalIgnoreCase))
                    {
                        pfcounter++;
                        logger._wLogger("Skipping .pf Files...");
                        logger._wLogger("Current PF Counter: " + pfcounter);
                    }
                    else if (info.FullName.StartsWith(windir, StringComparison.OrdinalIgnoreCase))
                    {
                        logger._wLogger("Skipping files in %windir% directory...");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(owner))
                        {
                            logger._wLogger("Datei ohne Besitzer gefunden, wird nicht geloggt...");
                        }
                        else
                        {
                            DisplayFiles(WatcherChangeTypes.Deleted, e.Name, owner);
                            counter++;
                        }
                    }
                });
               

            }
            catch (Exception ex)
            {
                log.ExLogger(ex);
            }
        }

        private async void Fsw_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    FileInfo info = new FileInfo(e.Name);
                    string owner = fo.GetOwnerofFile(e.Name);
                    string windir = Environment.GetEnvironmentVariable("windir");
                    logger._wLogger("Found windir under" + windir);

                    if (info.Name == "entries.log" || info.Name == "log.log" || info.Name == "error.log")
                    {

                    }
                    else if (info.Extension.Equals(".pf", StringComparison.OrdinalIgnoreCase))
                    {
                        pfcounter++;
                        logger._wLogger("Skipping .pf Files...");
                        logger._wLogger("Current PF Counter: " + pfcounter);
                    }
                    else if (info.FullName.StartsWith(windir, StringComparison.OrdinalIgnoreCase))
                    {
                        logger._wLogger("Skipping files in %windir% directory...");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(owner))
                        {
                            logger._wLogger("Datei ohne Besitzer gefunden, wird nicht geloggt...");
                        }
                        else
                        {
                            DisplayFiles(WatcherChangeTypes.Deleted, e.Name, owner);
                            counter++;
                        }
                    }
                });
                



            }
            catch (Exception ex)
            {
                log.ExLogger(ex);
            }
        


        }

        private async void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Run(() => {

                    FileInfo info = new FileInfo(e.Name);
                    string owner = fo.GetOwnerofFile(e.Name);
                    string windir = Environment.GetEnvironmentVariable("windir");
                    logger._wLogger("Found windir under" + windir);

                    if (info.Name == "entries.log" || info.Name == "log.log" || info.Name == "error.log")
                    {

                    }
                    else if (info.Extension.Equals(".pf", StringComparison.OrdinalIgnoreCase))
                    {
                        pfcounter++;
                        logger._wLogger("Skipping .pf Files...");
                        logger._wLogger("Current PF Counter: " + pfcounter);
                    }
                    else if (info.FullName.StartsWith(windir, StringComparison.OrdinalIgnoreCase))
                    {
                        logger._wLogger("Skipping files in %windir% directory...");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(owner))
                        {
                            logger._wLogger("Datei ohne Besitzer gefunden, wird nicht geloggt...");
                        }
                        else
                        {
                            DisplayFiles(WatcherChangeTypes.Deleted, e.Name, owner);
                            counter++;
                        }
                    }
                });
               


            }
            catch (Exception ex)
            {
                log.ExLogger(ex);
            }
        }
        */
        private void InitializeFileSystemWatcher()
        {
            //fsw.Path = @"C:\";
            fsw.IncludeSubdirectories = true;
            fsw.Filter = "*.*";
            fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            fsw.Changed += OnFileSystemEvent;
            fsw.Created += OnFileSystemEvent;
            fsw.Deleted += OnFileSystemEvent;
            fsw.Renamed += OnFileSystemEvent;

            fsw.EnableRaisingEvents = true;
        }

        private async void OnFileSystemEvent (object sender, FileSystemEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    FileInfo info = new FileInfo(e.FullPath);
                    string owner = fo.GetOwnerofFile(e.FullPath);

                    if (info.Name == "entries.log" || info.Name == "log.log" || info.Name == "error.log")
                    {
                        // Skip log files
                    }
                    else if (info.Extension.Equals(".pf", StringComparison.OrdinalIgnoreCase))
                    {
                        pfcounter++;
                        logger._wLogger("Skipping .pf Files...");
                        logger._wLogger("Current PF Counter: " + pfcounter);
                    }
                    else if (info.FullName.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.Windows), StringComparison.OrdinalIgnoreCase))
                    {
                        logger._wLogger("Skipping files in Windows directory...");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(owner))
                        {
                            //logger._wLogger("Datei ohne Besitzer gefunden, wird nicht geloggt...");
                            DisplayFiles(e.ChangeType, e.FullPath,"");
                            counter++;
                        }
                        else
                        {
                            DisplayFiles(e.ChangeType, e.FullPath, owner);
                            counter++;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                log.ExLogger(ex);
            }
        }

        void DisplayFiles ( WatcherChangeTypes watcherTypes, string FileName, string owner, string oldname = null)
        {

            Logger logger = new Classes.Logging.Logger();

            try
            {
                /*if (watcherTypes == WatcherChangeTypes.Changed)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, FileName, watcherTypes.ToString()), watcherTypes, owner); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { log.LogEntrys(FileName, DateTime.Now, watcherTypes); }));
                }
                else if (watcherTypes == WatcherChangeTypes.Created)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, FileName, watcherTypes.ToString()), watcherTypes,owner); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { log.LogEntrys(FileName, DateTime.Now, watcherTypes); }));
                }
                else if (watcherTypes == WatcherChangeTypes.Deleted)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, FileName, watcherTypes.ToString()), watcherTypes,owner); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { log.LogEntrys(FileName, DateTime.Now, watcherTypes); }));
                }
                else if (watcherTypes == WatcherChangeTypes.Renamed)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AddtoList(string.Format("{0} -> {1} - {2}", DateTime.Now, FileName, watcherTypes.ToString()), watcherTypes,owner); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { AutoScroll(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { DisplayCounter(); }));
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => { log.LogEntrys(FileName, DateTime.Now, watcherTypes); }));
                }*/

                Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
                {
                    AddtoList($"{DateTime.Now} -> {FileName} - {watcherTypes}", watcherTypes, owner);
                    AutoScroll();
                    DisplayCounter();
                    log.LogEntrys(FileName, DateTime.Now, watcherTypes);
                }));
            }
            catch (Exception ex)
            {
                log.ExLogger(ex);
            }
        }

        public void AddtoList(string text, WatcherChangeTypes types, string owner)
        {
            string addlist = text + owner;

            switch (types)
            {
                case WatcherChangeTypes.Changed:
                    lstview_anzeige.Items.Add(addlist);
                    break;
                case WatcherChangeTypes.Created:
                    lstview_anzeige.Items.Add(addlist);
                    break;
                case WatcherChangeTypes.Deleted:
                    lstview_anzeige.Items.Add(addlist);
                    break;
                case WatcherChangeTypes.Renamed:
                    lstview_anzeige.Items.Add(addlist);
                    break;
                default:
                    lstview_anzeige.Items.Add(addlist);
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
            logger.ClearAllTheGarbage();
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
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DirectoryWatch_Click (object sender, RoutedEventArgs e)
        {
            DirectoryWatcher watcher = new DirectoryWatcher();
            watcher.Show();
        }

        private void cmb_festplatten_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Alle verfügbaren Festplatten herausfinden, in eine Liste speichern
                DriveInfo[] drives = DriveInfo.GetDrives();
                int counter = 0;

                foreach (var d in drives)
                {
                    switch (d.DriveType)
                    {
                        case DriveType.CDRom:
                            logger._wLogger("DVD / CD ROM found... aborting");
                            counter++;
                            break;
                        case DriveType.Unknown:
                            logger._wLogger("Unknown Drive found, aborting");
                            counter++;
                            break;
                        case DriveType.Fixed:
                            cmb_festplatten.Items.Add(d + d.VolumeLabel);
                            counter++;
                            break;
                        case DriveType.Network:
                            cmb_festplatten.Items.Add(d + d.VolumeLabel);
                            counter++;
                            break;
                        default:
                            logger._wLogger(" No ready Drives found... aborting...");
                            break;
                    }

                }
                log._wLogger("Found " + counter + " Drives....");
            }
            catch (Exception ex)
            {
                log._eLogger(ex);
            }
        }

        public void Stop()
        {
            fsw.EnableRaisingEvents = false;
            lbl_Messages.Content = "Überwachung beendet";
            
        }

        private void lstview_anzeige_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(lstview_anzeige.SelectedItem == null)
                {
                    MessageBox.Show("Please select an item to show the details!", "No item selected", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedItem = lstview_anzeige.SelectedItem;
                
                if (selectedItem == null)
                {
                    MessageBox.Show("The selected item is either invalid or not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Beispiel: Anzeigen der Details des ausgewählten Elements
                string itemContent = selectedItem.ToString();
                string[] details = itemContent.Split(new[] { " -> ", " - " }, StringSplitOptions.None);

                if (details.Length >= 3)
                {
                    string date = details[0];
                    string fileName = details[1];
                    string action =  details[2];

                    //get only the filename from the filename variable
                    FileInfo fileInfo = new FileInfo(fileName);
                    string extension = fileInfo.Extension;

                    /*if (string.IsNullOrEmpty(extension))
                    {
                        string message = $"Date: {date}\nFileName: {fileName}\nAction: {action}\nType: Directory";
                        MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        int extensionIndex = fileName.LastIndexOf(extension, StringComparison.OrdinalIgnoreCase);
                        int lastIndex = fileName.LastIndexOf("\\", extensionIndex, StringComparison.OrdinalIgnoreCase);

                        string fileNameWithoutPath = fileName.Substring(lastIndex + 1);

                        string message = $"Date: {date}\nFileName: {fileNameWithoutPath}\nAction: {action}\nType: File";
                        MessageBox.Show(message, $"{fileNameWithoutPath}", MessageBoxButton.OK, MessageBoxImage.Information);
                    }*/

                    string type = string.IsNullOrEmpty(extension) ? "Directory" : "File";
                    var fileDetailsWindow = new FiileDetailsWindow(date, fileName, action, type);
                    fileDetailsWindow.ShowDialog();
                }
                else if(details.Length == 2)
                {
                    string date = details[0];
                    string action = details[2];

                    var fileDetailsWindow = new FiileDetailsWindow(date, string.Empty, action, "Unknown");
                    fileDetailsWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Can´t find fileinformation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                


            }
            catch (Exception ex)
            {
                log._eLogger(ex);
                MessageBox.Show(ex.Message);
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

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void SetPath_Click(object sender, RoutedEventArgs e)
        {
            SetPath sPath = new SetPath();
            sPath.Show();
        }

        private void FWOA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", Classes.Statics.appdata + @"\FileWatcher");
            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
            }
            
        }

        private void GoToLogOrdner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ordner = FIleOperations.getLoggerDir();
                Process.Start("explorer.exe", ordner);
            }
            catch ( Exception ex)
            {
                log.ExLogger(ex);
            }
            
        }

        private void LoDa_Click(object sender, RoutedEventArgs e)
        {
            ShowLog sLog = new ShowLog();
            sLog.Show();
        }

        private void lbl_ordner_clean_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Logs_Bereinigung logs_Bereinigung = new Logs_Bereinigung();
            logs_Bereinigung.ShowDialog();
        }

        private void lbl_ShowChangelog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Changelog log = new Changelog();
            log.ShowDialog();
        }

        private void ShowInfra(object sender, RoutedEventArgs e)
        {
            ShowInfra infra = new ShowInfra();
            infra.Show();
        }

        private void lbl_Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FileWatcher.FileWatcher_Settings settings = new FileWatcher_Settings();
            settings.Show();
        }
    }
}