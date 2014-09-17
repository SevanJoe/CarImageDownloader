using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HtmlAgilityPack;

using CarImageDownloader.CarData;
using CarImageDownloader.Excel;

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
            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + WebConstants.PHOTO_URL);
            HtmlNodeCollection brandNodes = htmlDocument.DocumentNode.SelectNodes(WebConstants.BRAND_NODE);
            if (brandNodes != null)
            {
                foreach (HtmlNode tempNode in brandNodes)
                {
                    HtmlNode brandNode = HtmlNode.CreateNode(tempNode.OuterHtml);
                    string brandUrl = brandNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;
                    char brandAlpha = brandNode.SelectSingleNode(WebConstants.BRAND_ALPHA).InnerText.ToCharArray()[0];
                    string brandNamePostFix = brandNode.SelectSingleNode(WebConstants.BRAND_NAME_POSTFIX).InnerText;
                    string brandName = brandNode.InnerText;
                    brandName = brandName.Substring(1, brandName.Length - brandNamePostFix.Length - 1);

                    CarBrand carBrand = new CarBrand(brandUrl);
                    carBrand.Alpha = brandAlpha;
                    carBrand.Name = brandName;
                    mCarBrandList.Add(carBrand);
                }
            }

            runBrandTasks();
        }

        private void runBrandTasks()
        {
            List<Thread> brandThreadList = new List<Thread>();
            foreach (CarBrand carBrand in mCarBrandList)
            {
                Thread brandThread = new Thread(new WebBrandTask(carBrand).Run);
                brandThreadList.Add(brandThread);
                brandThread.Start();
            }
            foreach (Thread brandThread in brandThreadList)
            {
                brandThread.Join();
            }
            new ExcelTask().WriteCarInfo(mCarBrandList);
        }
    }
}
