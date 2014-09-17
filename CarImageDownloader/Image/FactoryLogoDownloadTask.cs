using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Image
{
    class FactoryLogoDownloadTask : ImageDownloadTask
    {
        public FactoryLogoDownloadTask(CarFactory carFactory)
        {
            mFilePath = Path.Combine(BASE_FILE_PATH, carFactory.CarBrand.Name, carFactory.Name);
            mFileName = Path.Combine(mFilePath, carFactory.Name + IMAGE_POSTFIX);
            carFactory.LogoPath = Path.Combine(Environment.CurrentDirectory, mFileName);
            initFile();

            mUrl = carFactory.LogoUrl;
        }
    }
}
