using System;
using System.Collections.Generic;
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

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für Errorbehandlung.xaml
    /// </summary>
    public partial class Errorbehandlung : Window
    {
        public Errorbehandlung()
        {
            InitializeComponent();
        }

        public static void DisplayError(Exception ex)
        {

            string[] value;

            value = Environment.GetCommandLineArgs();

            foreach (string argument in value)
            {
                if (argument == "-debug")
                {
                    Errorbehandlung behandlung = new Errorbehandlung();

                    string errormessage = ex.Message;
                    string stacktrace = ex.StackTrace;


                    behandlung.txt_message.Text = errormessage;
                    behandlung.txt_stacktrace.Text = stacktrace;

                    behandlung.Show();
                }
                else
                {
                    //Pech gehabt, dann wird nix ausgegeben!
                }
            }



       }


    }



    
}
