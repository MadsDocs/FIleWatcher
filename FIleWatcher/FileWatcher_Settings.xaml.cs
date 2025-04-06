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

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für FileWatcher_Settings.xaml
    /// </summary>
    public partial class FileWatcher_Settings : Window
    {
        public FileWatcher_Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string databaseserver = txt_database.Text;
            string username = txt_username.Text;
            string password = txt_password.Text;
            string database = txt_database.Text;

            //Check if all fields are filled
            if (databaseserver == "" || username == "" || password == "" || database == "")
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                //Save the settings

            }
        }

        private static void EncryptPassword(string password)
        {
            //Encrypt the password

        }
    }
}
