using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Image
{
    class CountryLogoDownloadTask : ImageDownloadTask
    {
        public CountryLogoDownloadTask(Country country)
        {
            mFilePath = Path.Combine(BASE_FILE_PATH + "Country");
            mFileName = Path.Combine(mFilePath, country.Name + IMAGE_POSTFIX);
            country.LogoPath = Path.Combine(Environment.CurrentDirectory, mFileName);
            initFile();

            mUrl = country.LogoUrl;
        }
    }
}
