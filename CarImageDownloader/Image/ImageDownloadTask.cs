using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.Image
{
    abstract class ImageDownloadTask
    {
        protected const string BASE_FILE_PATH = @"..\..\..\Images\";
        protected const string IMAGE_POSTFIX = ".png";

        protected string mFilePath;
        protected string mFileName;
        protected string mUrl;

        protected void initFile()
        {
            Directory.CreateDirectory(mFilePath);

            if (!File.Exists(mFileName))
            {
                File.Create(mFileName).Close();
            }
        }

        public void Download()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(mUrl);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if ((httpWebResponse.StatusCode == HttpStatusCode.OK || httpWebResponse.StatusCode == HttpStatusCode.Moved ||
                httpWebResponse.StatusCode == HttpStatusCode.Redirect) && httpWebResponse.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {
                using (Stream inputStream = httpWebResponse.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(mFileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
            }
        }
    }
}
