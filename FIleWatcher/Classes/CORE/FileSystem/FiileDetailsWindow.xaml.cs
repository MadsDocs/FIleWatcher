using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            var model = new FileDetailsViewModel
            {
                Date = date,
                FileName = fileName,
                Action = action,
                Type = type,
                FilePath = fileName
            };

            DataContext = model;

        }
        private void LinkFilePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Process.Start("explorer.exe", $"/select,\"{FilePathLink.NavigateUri.LocalPath}\"");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open file in Explorer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilePathLink_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not FileDetailsViewModel model || string.IsNullOrWhiteSpace(model.FilePath))
            {
                MessageBox.Show("No file path available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (File.Exists(model.FilePath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"/select,\"{model.FilePath}\"",
                        UseShellExecute = true
                    });
                }
                else if (Directory.Exists(model.FilePath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"\"{model.FilePath}\"",
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("The target path no longer exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open File Explorer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
