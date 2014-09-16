using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Image
{
    class TypeImageDownloadTask : ImageDownloadTask
    {
        public TypeImageDownloadTask(CarType carType)
        {
            mFilePath = Path.Combine(BASE_FILE_PATH, carType.CarFactory.CarBrand.Name, carType.CarFactory.Name);
            mFileName = Path.Combine(mFilePath, carType.Name + IMAGE_POSTFIX);
            initFile();

            mUrl = carType.ImageUrl;
        }
    }
}
