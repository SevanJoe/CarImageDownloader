using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarImageDownloader.CarData
{
    class Country
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string LogoPath { get; set; }

        public Country(string name)
        {
            Name = name;
        }
    }
}
