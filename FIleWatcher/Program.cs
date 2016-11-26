using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FIleWatcher
{
    class Program
    {
       
        static void Main(string[] args)
        {
            do
            {
                FSW.INIT();

            } while (Console.Read() != 'q');
        }


       
    }
}
