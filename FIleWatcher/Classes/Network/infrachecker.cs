using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Windows.Forms;

namespace FileWatcher.Classes.Network
{
    class infrachecker
    {
        public void checkInfra()
        {
            //Diese Methode soll checken ob alle Server verfügbar sind
            //Derzeit ist es genau ein Server (FTP) aber vl. werden es ja mal mehr...


            try
            {
                FtpWebRequest testrequest = (FtpWebRequest)WebRequest.Create("84.115.70.116");
                testrequest.Credentials = new NetworkCredential("", "");
                testrequest.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)testrequest.GetResponse();

                if ( response.StatusCode == FtpStatusCode.NeedLoginAccount)
                {
                    MessageBox.Show("FTP Server is up");
                }
                else if ( response.StatusCode == FtpStatusCode.CantOpenData)
                {
                    MessageBox.Show("FTP Server is not up");
                }
                    

            }
            catch ( Exception ex)
            {

            }
        }
    }
}
