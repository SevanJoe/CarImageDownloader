using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PinYinUtils;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Image
{
    class BrandLogoDownloadTask : ImageDownloadTask
    {
        public BrandLogoDownloadTask(CarBrand carBrand)
        {
            mFilePath = Path.Combine(BASE_FILE_PATH, BRAND_DIR, PINYIN_DIR); //carBrand.Name);
            mFileName = Path.Combine(mFilePath, PinYinConverter.Get(carBrand.Name) + IMAGE_POSTFIX);
            carBrand.LogoPath = Path.Combine(Environment.CurrentDirectory , mFileName);
            initFile();

            mUrl = carBrand.LogoUrl;
        }
    }
}
