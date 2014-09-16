using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Image
{
    class BrandLogoDownloadTask : ImageDownloadTask
    {
        public BrandLogoDownloadTask(CarBrand carBrand)
        {
            mFilePath = Path.Combine(BASE_FILE_PATH, carBrand.Name);
            mFileName = Path.Combine(mFilePath, carBrand.Name + IMAGE_POSTFIX);
            initFile();

            mUrl = carBrand.LogoUrl;
        }
    }
}
