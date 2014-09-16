using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.CarData
{
    class CarFactory
    {
        public CarBrand CarBrand { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string OfficialSite { get; set; }

        public CarFactory(CarBrand carBrand)
        {
            CarBrand = carBrand;
        }
    }
}
