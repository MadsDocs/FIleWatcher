using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FileWatcher.Classes.CORE.FileSystem
{
    /// <summary>
    /// Interaction logic for FiileDetailsWindow.xaml
    /// </summary>
    public partial class FiileDetailsWindow : Window
    {
        public FiileDetailsWindow(string date, string fileName,string action, string type)
        {
            InitializeComponent();
            txtDate.Text = date;
            txtFileName.Text = fileName;
            txtAction.Text = action;
            txtType.Text = type;
            linkFilePath.NavigateUri = new Uri(fileName);

            //disable hyperlink when action is deleted
            if (action == "Deleted")
            {
                linkFilePath.IsEnabled = false;  
                linkFilePath.ToolTip = "File not found";
                linkFilePath.Foreground = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                linkFilePath.IsEnabled = true;
            }

        }
        private void LinkFilePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", $"/select,\"{linkFilePath.NavigateUri.LocalPath}\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open file in Explorer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
