using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.CarData
{
    class CarType
    {
        public CarFactory CarFactory { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }

        public CarType(CarFactory carFactory)
        {
            CarFactory = carFactory;
        }
    }
}
