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
        public const string PHOTO_URL = "frag/priceInc/photo_1.inc";

        public const string HREF = "href";
        public const string LINK_HREF = "//a/@href";
        public const string SRC = "src";
        public const string IMAGE_SRC = "//img/@src";
        public const string EM = "em";
        public const string SCRIPT_SRC = "//script/@src";

        public const string BRAND_NODE = "//*[@class=\"nav-class-l nav-class-close\"]";
        public const string BRAND_ALPHA = "//*[@class=\"l\"]";
        public const string BRAND_NAME_POSTFIX = "//*[@class=\"n\"]";

        public const string BRAND_LOGO = "//*[@class=\"mer-logo\"]";
        public const string BRAND_OFFICIAL_SITE = "//*[@class=\"mer-link\"]";
        public const string BRAND_COUNTRY = "//*[@class=\"mer-countries\"]";
        public const string BRAND_LIST = "//*[@class=\"fl pr ta-l md-nav-main\"]";

        public const string FACTORY_NODE = "//*[@class=\"nav-class-dl\"]";
    }
}
