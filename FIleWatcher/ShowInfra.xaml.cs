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

using FileWatcher.Classes.Network;
using FileWatcher.Classes.Logging;
using System.Media;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für ShowInfra.xaml
    /// </summary>
    public partial class ShowInfra : Window
    {
        public ShowInfra()
        {
            InitializeComponent();
            txt_status.IsEnabled = false;
            FileWatcher.Classes.Network.infrachecker infrachecker = new infrachecker();

            string status = infrachecker.checkInfra();
            txt_status.Text = status;
        }


    }
}
