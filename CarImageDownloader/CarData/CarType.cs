using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.CarData
{
    class CarType
    {
        public CarBrand CarBrand { get; set; }
        public string CarTypeName { get; set; }
        public string CarTypeImageUrl { get; set; }

        public CarType(CarBrand carBrand)
        {
            CarBrand = carBrand;
        }
    }
}
