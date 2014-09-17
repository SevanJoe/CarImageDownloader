using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.Excel
{
    class ExcelConstants
    {
        public const string FILE_NAME = @"..\..\..\汽车车型.xlsx";
        public const string SHEET_NAME = "汽车车型";

        public static readonly string[] SHEET_HEADERS = { "索引", "品牌", "标志", "官方网站", "国家", "国家标志", "厂商", "厂商标志", "厂商官网", "车型", "车型图片" };

        public const int BRAND_LOGO_SIZE = 160;
        public const int COUNTRY_LOGO_WIDTH = 19;
        public const int COUNTRY_LOGO_HEIGHT = 11;
        public const int CAR_IMAGE_WIDTH = 180;
        public const int CAR_IMAGE_HEIGHT = 120;

        public const string NO_IMAGE = "暂无图片";
    }
}
