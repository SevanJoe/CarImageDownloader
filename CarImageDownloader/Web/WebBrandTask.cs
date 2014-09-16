using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Web
{
    class WebBrandTask
    {
        private CarBrand mCarBrand;
        private List<CarFactory> mCarFactoryList;

        public WebBrandTask(CarBrand carBrand)
        {
            mCarBrand = carBrand;
            mCarFactoryList = new List<CarFactory>();
        }

        public void Run()
        {
            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + mCarBrand.Url);
            HtmlNode logoNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_LOGO).OuterHtml);
            mCarBrand.LogoUrl = logoNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;
            HtmlNode officialSiteNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_OFFICIAL_SITE).OuterHtml);
            mCarBrand.OfficialSite = officialSiteNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;
            HtmlNode countryNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_COUNTRY).OuterHtml);
            mCarBrand.Country = new Country(countryNode.InnerText.Substring(countryNode.SelectSingleNode(WebConstants.EM).InnerText.Length));
            mCarBrand.Country.LogoUrl = countryNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;
        }
    }
}
