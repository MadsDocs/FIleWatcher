using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

using FileWatcher.Classes;
using System.IO;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für Changelog.xaml
    /// </summary>
    public partial class Changelog : Window
    {
        public Changelog()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            webBrowser_changelog.Navigate("https://themadbrainz.net/FileWatcher/Changelog.txt");

        }
    }
}
