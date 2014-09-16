﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.CarData
{
    class CarBrand
    {
        public string Url { get; set; }
        public char Alpha { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string OfficialSite { get; set; }
        public Country Country { get; set; }

        public CarBrand(string url)
        {
            Url = url;
        }
    }
}
