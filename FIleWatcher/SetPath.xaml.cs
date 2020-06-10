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
using System.Diagnostics;
using FileWatcher;
using FileWatcher.Classes.Logging;

using FileWatcher.Classes;

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für SetPath.xaml
    /// </summary>
    public partial class SetPath : Window
    {

        private static Logger log = new Logger();

        public SetPath()
        {
            Classes.FileSystem.Init init = new Classes.FileSystem.Init();
            Classes.Statics st = new Statics();

            InitializeComponent();
            string helping = " Dieser Dialog soll Ihnen helfen ein Verzeichnis für den FileWatcher \r\n zu erstellen, damit dieser gefundene Einträge in eine Datei \r\n speichern kann!";
            

            txtbl_help.Content = helping;
            lbl_version.Content = init.Fwversion;


            if (File.Exists(Statics.appdata + @"\FileWatcher\save"))
                MessageBox.Show(@" Log Ordner wurde eingerichtet! Sollten Sie den Ort wechseln wollten, können Sie einfach die save Datei unter %appdata%\save editieren!", "Log Ordner wurde eingerichtet", MessageBoxButton.OK, MessageBoxImage.Error);




        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Hier mit wird der Pfad gesetzt damit der Logger und die Log Entries Methoden loggen können
                string pfad = txt_pfad.Text;

                FileInfo info = new FileInfo(pfad);
                string ext = info.Extension;

               

                    // Überprüfen ob pfad eine Extension hat ( zB.: C:\Test )
                    if (ext != string.Empty)
                    {
                        MessageBox.Show(" Bitte einen Ordnerpfad angeben! ");
                    }
                    // Wenn nicht, dann wird der Ordner erstellt!
                    else
                    {
                        if (pfad == string.Empty)
                        {
                            MessageBox.Show("Bitte geben Sie einen validen Pfad ein!", "Invalid Pfad", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            //Case I: Ordner wurde noch nicht erstellt sollte aber erstellt werden...
                            if (!Directory.Exists(pfad))
                            {
                                MessageBoxResult result = MessageBox.Show(" Ordner existiert noch nicht, soll dieser erstellt werden? ", " Order noch nicht existent", MessageBoxButton.YesNo, MessageBoxImage.Information);

                                if (result == MessageBoxResult.Yes)
                                {
                                    Directory.CreateDirectory(pfad);
                                    File.AppendAllText(Classes.Statics.appdata + @"\FileWatcher\save", pfad);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show(" Ordner Erstellung abgebrochen, damit funktioniert das Logging nicht mehr und es werden keine Einträge mitgeloggt!");
                                }

                            }
                            else
                            {
                                // Case II: Angegebener Ordner ist NICHT leer...
                                if (Directory.GetFiles(pfad).Length > 0)
                                {
                                    MessageBoxResult result = MessageBox.Show("Der angegebene Ordner ist nicht leer, wollen Sie diesen trotz dem benutzen?", "Ordner ist nicht leer", MessageBoxButton.YesNo, MessageBoxImage.Information);

                                    if (result == MessageBoxResult.Yes)
                                    {
                                        if (File.Exists(Classes.Statics.appdata + @"\FileWatcher\save"))
                                        {
                                            File.Delete(Classes.Statics.appdata + @"\FileWatcher\save");
                                            File.AppendAllText(Classes.Statics.appdata + @"\FileWatcher\save", pfad);
                                            Close();
                                        }
                                        else
                                        {
                                            File.AppendAllText(Classes.Statics.appdata + @"\FileWatcher\save", pfad);
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        pfad = null;
                                        txt_pfad.Clear();

                                        MessageBox.Show("Ordner Erstellung abgebrochen, geben Sie bitte einen anderen Ordner an!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                        Close();
                                    }
                                }
                                else
                                {
                                    if (File.Exists(Classes.Statics.appdata + @"\FileWatcher\save"))
                                    {
                                        File.Delete(Classes.Statics.appdata + @"\FileWatcher\save");
                                        File.AppendAllText(Classes.Statics.appdata + @"\FileWatcher\save", pfad);
                                        Close();
                                    }
                                    else
                                    {
                                        File.AppendAllText(Classes.Statics.appdata + @"\FileWatcher\save", pfad);
                                        Close();
                                    }
                                }
                            }
                        }
                    
                  }
                }
            catch (Exception ex)
            {
                MessageBox.Show(" Konnte den Pfad: " + txt_pfad.Text + " nicht finden!", " Fehler beim erstellen der save Datei!", MessageBoxButton.OK, MessageBoxImage.Error);
                log.ExLogger(ex);

            }
        

        }

        private bool SaveSanityCheck()
        {
            // Check: Es kann vorkommen das der SetPath Dialog auch einfach so gestartet wird,
            // in dem Fall wäre es blöd wenn dann in dem save File drinnen stehen würd:
            // F:\FWLF:\FWL
            // Deshalb sollten wir dem Benutzer anzeigen dass das save File verändert worden ist
            // und dass das File geändert werden muss!
            
            if ( !File.Exists ( Statics.appdata + @"\save"))
            {
                // Tja, dann starten wir das Programm das erste mal!
            }
            else
            {
                string line = "";
                string content = "";
                int counter = 0;
                StreamReader saveReader = new StreamReader(Statics.appdata + @"\save");

                while ((line = saveReader.ReadLine()) != null)
                {
                    line = content;
                    counter++;

                    if ( counter > 1)
                    {
                        content.Remove(counter);
                    }

                }
            }
            return true;
        }
    }
}
