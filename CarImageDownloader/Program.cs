﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarImageDownloader.Web;

namespace CarImageDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            new WebTask().Run();
        }
    }
}
