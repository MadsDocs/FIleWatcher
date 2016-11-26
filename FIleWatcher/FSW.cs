using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using System.IO;

namespace FIleWatcher
{
    class FSW
    {

        static int zaehler = 0;

        public static void INIT()
        {
            int maxcheight = Console.LargestWindowHeight /2;
            int maxcwidth = Console.LargestWindowWidth /2;
            Console.SetWindowSize(maxcwidth, maxcheight);

            zaehler++;
            FileSystemWatcher fsw = new FileSystemWatcher();
            fsw.Path = @"C:\";
            fsw.IncludeSubdirectories = true;
            fsw.Filter = "*.*";
            fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fsw.EnableRaisingEvents = true;
            

            fsw.Changed += Fsw_Changed;
            fsw.Created += Fsw_Created;
            fsw.Deleted += Fsw_Deleted;
            fsw.Renamed += Fsw_Renamed;
            fsw.Error += Fsw_Error;
            fsw.Disposed += Fsw_Disposed;
        }

        private static void Fsw_Disposed(object sender, EventArgs e)
        {
            //Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(DateTime.Now + "\tDisposed: " + e.ToString());
        }

        private static void Fsw_Error(object sender, ErrorEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(DateTime.Now + "\tError: " + e.GetException());
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Fsw_Renamed(object sender, RenamedEventArgs e)
        {
           // FileInfo fInfo = new FileInfo(@"C:\" + e.Name);
            //string ext = fInfo.Extension;
            //long length = fInfo.Length;


            //Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(DateTime.Now + "\tErstellt: " + e.Name);
           // Console.WriteLine(DateTime.Now + "\tExtension: " + ext);
           // Console.WriteLine(DateTime.Now + "\tGröße: " + length);

        }

        private static void Fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            //Console.ForegroundColor = ConsoleColor.;
            Console.WriteLine(DateTime.Now + "\tGelöscht: " + e.Name);
        }

        private static void Fsw_Created(object sender, FileSystemEventArgs e)
        {

            try
            {
                FileInfo fInfo = new FileInfo(@"C:\" + e.Name);
                string ext = fInfo.Extension;
                long length = fInfo.Length;


                //Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(DateTime.Now + "\tErstellt: " + e.Name);
                Console.WriteLine(DateTime.Now + "\tExtension: " + ext);
                Console.WriteLine(DateTime.Now + "\tGröße: " + length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

        }

        private static void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            //FileInfo fInfo = new FileInfo(@"C:\" + e.Name);
            //string ext = fInfo.Extension;
            //long length = fInfo.Length;


            //Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(DateTime.Now + "\tErstellt: " + e.Name);
            //Console.WriteLine(DateTime.Now + "\tExtension: " + ext);
            //Console.WriteLine(DateTime.Now + "\tGröße: " + length);
        }
    }
}
