using FileWatcher.Classes.FileSystem;
using FileWatcher.Classes.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher.Classes.CORE.FW_Network
{
    class Downloader
    {
        public static void DownloadConfig(string address)
        {
            try
            {
                /*string link = "https://baronie.themadbrainz.net/filewatcher/App.config";
                WebClient client = new WebClient();
                client.DownloadFile(link, Init.appdata + @"\filewatcher\config\App.config");
                */
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.ExLogger(ex);
            }
        }
    }
}
