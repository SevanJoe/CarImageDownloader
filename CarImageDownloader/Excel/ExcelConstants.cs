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

        public static readonly string[] SHEET_HEADERS = { "索引", "品牌", "官方网站", "国家", "厂商", "厂商官网", "车型", "指导价最低", "指导价最高" };
    }
}
