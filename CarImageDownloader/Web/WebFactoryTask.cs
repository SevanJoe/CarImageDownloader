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
    class WebFactoryTask
    {
        private CarFactory mCarFactory;

        public WebFactoryTask(CarFactory carFactory)
        {
            mCarFactory = carFactory;
        }

        public void Run()
        {
            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + mCarFactory.Url);
            HtmlNode logoNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_LOGO).OuterHtml);
            mCarFactory.LogoUrl = logoNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;
            new FactoryLogoDownloadTask(mCarFactory).Download();

            HtmlNode officialSiteNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_OFFICIAL_SITE).OuterHtml);
            mCarFactory.OfficialSite = officialSiteNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;

            HtmlNodeCollection typeNodes = htmlDocument.DocumentNode.SelectNodes(WebConstants.TYPE_NODE);
            if (typeNodes != null)
            {
                foreach (HtmlNode tempNode in typeNodes)
                {
                    HtmlNode typeNode = HtmlNode.CreateNode(tempNode.OuterHtml);
                    CarType carType = new CarType(mCarFactory);
                    HtmlNode nameNode = HtmlNode.CreateNode(typeNode.SelectSingleNode(WebConstants.TYPE_NAME).OuterHtml);
                    carType.Name = nameNode.SelectSingleNode(WebConstants.LINK_HREF).InnerText;
                    HtmlNode imageNode = HtmlNode.CreateNode(typeNode.SelectSingleNode(WebConstants.TYPE_IMAGE).OuterHtml);
                    carType.ImageUrl = imageNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;
                    new TypeImageDownloadTask(carType).Download();

                    mCarFactory.CarTypeList.Add(carType);
                }
            }
        }
    }
}
