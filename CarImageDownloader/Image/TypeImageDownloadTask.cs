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
    class TypeImageDownloadTask : ImageDownloadTask
    {
        public TypeImageDownloadTask(CarType carType)
        {
            mFilePath = Path.Combine(BASE_FILE_PATH, TYPE_DIR, PINYIN_DIR); //carType.CarFactory.CarBrand.Name, carType.CarFactory.Name);
            mFileName = Path.Combine(mFilePath, PinYinConverter.Get(carType.CarFactory.Name + "_" + carType.Name) + IMAGE_POSTFIX);
            carType.ImagePath = Path.Combine(Environment.CurrentDirectory, mFileName);
            initFile();

            mUrl = carType.ImageUrl;
        }
    }
}
