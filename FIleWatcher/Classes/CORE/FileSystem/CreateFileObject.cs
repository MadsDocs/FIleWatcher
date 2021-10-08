using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using FileWatcher.Classes.Logging;
using FileWatcher.Classes.FileSystem;

namespace FileWatcher.Classes.CORE.FileSystem
{

    class CreateFileObject
    {
        private static Logger log = new Logger();
        private static FIleOperations fo = new FIleOperations();
        public string createFileObject (string file)
        {
            try
            {
                //Dies ist die Methode welche aus einem gegebenen File ein FileObject macht.
                FileInfo finfo = new FileInfo(file);
                string path = finfo.FullName;
                long lenghth = finfo.Length;
                string creationTime = finfo.CreationTime.ToString();
                string lastwriteTime = finfo.LastWriteTime.ToString();
                string lasatAccessTime = finfo.LastAccessTime.ToString();
                string attributes = finfo.Attributes.ToString();

                string owner = fo.GetOwnerofFile(file);

                return path + lenghth + creationTime + lastwriteTime + lasatAccessTime + attributes + owner;

            }
            catch (Exception ex)
            {
                log._eLogger(ex);
                return "";
            }
        }

    }
}
