using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Windows.Forms;

using FileWatcher.Classes.Logging;

namespace FileWatcher.Classes.Network
{
    class Upload
    {
        public void UploadErrorLog ( string pathtoerror)
        {
            try
            {


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://84.115.70.116");
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential("upload", "a3hJDLdFIKsMILIpCQywFDjAc");
                byte[] contents;
                using (StreamReader errorlog = new StreamReader(pathtoerror))
                {
                    contents = Encoding.UTF8.GetBytes(errorlog.ReadToEnd());
                }

                request.ContentLength = contents.Length;

                using (Stream requeststream = request.GetRequestStream())
                {
                    requeststream.Write(contents, 0, contents.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    MessageBox.Show("error.log was successfully uploaded!");
                }
            }
            catch ( Exception ex)
            {
                Logger log = new Logger();
                log._eLogger(ex);
            }


        }
    }
}
