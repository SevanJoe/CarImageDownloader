using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using CarImageDownloader.CarData;
using CarImageDownloader.Image;

namespace CarImageDownloader.Web
{
    class WebBrandTask
    {
        private CarBrand mCarBrand;

        public WebBrandTask(CarBrand carBrand)
        {
            mCarBrand = carBrand;
        }

        public void Run()
        {
            HtmlWeb htmlWeb = new HtmlWeb();

            HtmlDocument htmlDocument = htmlWeb.Load(WebConstants.BASE_URL + mCarBrand.Url);
            HtmlNode logoNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_LOGO).OuterHtml);
            mCarBrand.LogoUrl = logoNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;
            new BrandLogoDownloadTask(mCarBrand).Download();

            HtmlNode officialSiteNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_OFFICIAL_SITE).OuterHtml);
            mCarBrand.OfficialSite = officialSiteNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;
            HtmlNode countryNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_COUNTRY).OuterHtml);
            mCarBrand.Country = new Country(countryNode.InnerText.Substring(countryNode.SelectSingleNode(WebConstants.EM).InnerText.Length));
            mCarBrand.Country.LogoUrl = countryNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;

            HtmlNode brandListNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_LIST).OuterHtml);
            mCarBrand.ListUrl = brandListNode.SelectSingleNode(WebConstants.SCRIPT_SRC).Attributes[WebConstants.SRC].Value;
            htmlDocument = htmlWeb.Load(WebConstants.BASE_URL + mCarBrand.ListUrl);
            HtmlNodeCollection factoryNodes = htmlDocument.DocumentNode.SelectNodes(WebConstants.FACTORY_NODE);
            if (factoryNodes != null)
            {
                foreach (HtmlNode tempNode in factoryNodes)
                {
                    HtmlNode factoryNode = HtmlNode.CreateNode(tempNode.OuterHtml);
                    CarFactory carFactory = new CarFactory(mCarBrand);
                    carFactory.Url = factoryNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;
                    carFactory.Name = factoryNode.InnerText;
                    mCarBrand.CarFactoryList.Add(carFactory);
                }
            }

            runFactoryTasks();
        }

        private void runFactoryTasks()
        {
            new WebFactoryTask(mCarBrand.CarFactoryList[0]).Run();
        }
    }
}
