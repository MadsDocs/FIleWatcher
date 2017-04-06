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

namespace FileWatcher
{
    /// <summary>
    /// Interaktionslogik für SetPath.xaml
    /// </summary>
    public partial class SetPath : Window
    {
        public SetPath()
        {
            InitializeComponent();
            lbl_Hinweis.Content = " Dieser Dialog soll Ihnen helfen ein Verzeichnis für den FileWatcher zu erstellen, \r\n"
                                    + " damit dieser gefundene Einträge in eine Datei speichern kann!";
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
                // 
                else
                {
                    if (pfad == string.Empty)
                    {
                        MessageBox.Show("Bitte geben Sie einen validen Pfad ein!", "Invalid Pfad", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (!Directory.Exists(pfad))
                        {
                            MessageBoxResult result = MessageBox.Show(" Ordner existiert noch nicht, soll dieser erstellt werden? ", " Order noch nicht existent", MessageBoxButton.YesNo, MessageBoxImage.Information);

                            if (result == MessageBoxResult.Yes)
                            {
                                Directory.CreateDirectory(pfad);
                                File.AppendAllText(Options.appdata + @"\FileWatcher\save", pfad);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(" Ordner Erstellung abgebrochen, damit funktioniert das Logging nicht mehr und es werden keine Einträge mitgeloggt!");
                            }

                        }
                        else
                        {
                            if (Directory.GetFiles(pfad).Length > 0)
                            {
                                MessageBoxResult result = MessageBox.Show("Der angegebene Ordner ist nicht leer, wollen Sie diesen trotz dem benutzen?", "Ordner ist nicht leer", MessageBoxButton.YesNo, MessageBoxImage.Information);

                                if (result == MessageBoxResult.Yes)
                                {
                                    if (File.Exists(Options.appdata + @"\FileWatcher\save"))
                                    {
                                        File.Delete(Options.appdata + @"\FileWatcher\save");
                                        File.AppendAllText(Options.appdata + @"\FileWatcher\save", pfad);
                                        this.Close();
                                    }
                                    else
                                    {
                                        File.AppendAllText(Options.appdata + @"\FileWatcher\save", pfad);
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    pfad = null;
                                    txt_pfad.Clear();

                                    MessageBox.Show("Ordner Erstellung abgebrochen, geben Sie bitte einen anderen Ordner an!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                    this.Close();
                                }
                            }
                            else
                            {
                                if (File.Exists(Options.appdata + @"\FileWatcher\save"))
                                {
                                    File.Delete(Options.appdata + @"\FileWatcher\save");
                                    File.AppendAllText(Options.appdata + @"\FileWatcher\save", pfad);
                                    this.Close();
                                }
                                else
                                {
                                    File.AppendAllText(Options.appdata + @"\FileWatcher\save", pfad);
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Konnte den Pfad: " + txt_pfad.Text + " nicht finden!", " Fehler beim erstellen der save Datei!", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(ex.Message);
            }

        }
    }
}
