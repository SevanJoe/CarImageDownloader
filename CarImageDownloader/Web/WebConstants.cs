using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.Web
{
    class WebConstants
    {
        public const string BASE_URL = "http://data.auto.ifeng.com/";

        public const string HREF = "href";
        public const string IMAGE_HREF = "//a[@href]";

        public const string PHOTO = "frag/priceInc/photo_1.inc";

        public const string BRAND_NODE = "//*[@class=\"nav-class-l nav-class-close\"]";
        public const string BRAND_ALPHA = "//*[@class=\"l\"]";
        public const string BRAND_NAME_POSTFIX = "//*[@class=\"n\"]";
    }
}
