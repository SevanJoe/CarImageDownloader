﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Web
{
    class WebTask
    {
        private List<CarBrand> mCarBrandList;

        public WebTask()
        {
            mCarBrandList = new List<CarBrand>();
        }

        public void Run()
        {
            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + WebConstants.PHOTO);
            HtmlNodeCollection brandNodes = htmlDocument.DocumentNode.SelectNodes(WebConstants.BRAND_NODE);
            if (brandNodes != null)
            {
                foreach (HtmlNode tempNode in brandNodes)
                {
                    HtmlNode brandNode = HtmlNode.CreateNode(tempNode.OuterHtml);
                    string brandUrl = brandNode.SelectSingleNode(WebConstants.IMAGE_HREF).Attributes[WebConstants.HREF].Value;
                    char brandAlpha = brandNode.SelectSingleNode(WebConstants.BRAND_ALPHA).InnerText.ToCharArray()[0];
                    string brandNamePostFix = brandNode.SelectSingleNode(WebConstants.BRAND_NAME_POSTFIX).InnerText;
                    string brandName = brandNode.InnerText;
                    brandName = brandName.Substring(1, brandName.Length - brandNamePostFix.Length - 1);

                    Console.WriteLine(brandAlpha + ": " + brandName);

                    CarBrand carBrand = new CarBrand(brandUrl);
                    carBrand.Alpha = brandAlpha;
                    carBrand.BrandName = brandName;
                    mCarBrandList.Add(carBrand);
                }
            }
        }
    }
}
