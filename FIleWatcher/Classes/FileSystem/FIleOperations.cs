using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FileWatcher.Classes.FileSystem
{
    class FIleOperations
    {
        public static string getFileExt( string path )
        {
            FileInfo fileext = new FileInfo(path);
            string ext = fileext.Extension;
            return ext;
        }


    }
}
