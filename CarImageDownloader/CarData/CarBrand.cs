using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.CarData
{
    class CarBrand
    {
        public char Alpha { get; set; }
        public string BrandName { get; set; }
        public string BrandLogoUrl { get; set; }
        public string BrandCountry { get; set; }
        public string BrandOfficialSite { get; set; }

        private string mBrandUrl;

        public CarBrand(string brandUrl)
        {
            mBrandUrl = brandUrl;
        }
    }
}
