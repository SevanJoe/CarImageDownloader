using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HtmlAgilityPack;

using CarImageDownloader.CarData;
using CarImageDownloader.Image;

namespace CarImageDownloader.Web
{
    class WebFactoryTask
    {
        private CarFactory mCarFactory;
        private List<CarType> mCarTypeList;

        public WebFactoryTask(CarFactory carFactory)
        {
            mCarFactory = carFactory;
            mCarTypeList = new List<CarType>();
        }

        public void Run()
        {
            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + mCarFactory.Url);
            HtmlNode logoNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_LOGO).OuterHtml);
            mCarFactory.LogoUrl = logoNode.SelectSingleNode(WebConstants.IMAGE_SRC).Attributes[WebConstants.SRC].Value;
            new Thread(new FactoryLogoDownloadTask(mCarFactory).Download).Start();

            HtmlNode officialSiteNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.BRAND_OFFICIAL_SITE).OuterHtml);
            mCarFactory.OfficialSite = officialSiteNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;

            addPageType(mCarFactory.Url);

            HtmlNode pagesNode = HtmlNode.CreateNode(htmlDocument.DocumentNode.SelectSingleNode(WebConstants.PAGE).OuterHtml);
            HtmlNodeCollection pageNodes = pagesNode.SelectNodes(WebConstants.PAGE_NODE);
            if (pageNodes != null)
            {
                foreach (HtmlNode tempNode in pageNodes)
                {
                    HtmlNode pageNode = HtmlNode.CreateNode(tempNode.OuterHtml);
                    String pageUrl = pageNode.SelectSingleNode(WebConstants.LINK_HREF).Attributes[WebConstants.HREF].Value;
                    addPageType(pageUrl);
                }
            }
        }

        private void addPageType(String pageUrl)
        {
            mCarTypeList.Clear();

            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + pageUrl);
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
                    new Thread(new TypeImageDownloadTask(carType).Download).Start();

                    mCarTypeList.Add(carType);
                    //mCarFactory.CarTypeList.Add(carType);
                }
            }

            String priceUrl = pageUrl.Replace(WebConstants.PHOTO, WebConstants.PRICE);
            setPrice(priceUrl);
        }

        private void setPrice(String priceUrl)
        {
            HtmlDocument htmlDocument = new HtmlWeb().Load(WebConstants.BASE_URL + priceUrl);
            HtmlNodeCollection typeNodes = htmlDocument.DocumentNode.SelectNodes(WebConstants.TYPE_NODE);
            if (typeNodes != null)
            {
                for (int i = 0; i < typeNodes.Count; ++i)
                {
                    HtmlNode typeNode = HtmlNode.CreateNode(typeNodes[i].OuterHtml);
                    HtmlNode dataNode = HtmlNode.CreateNode(typeNode.SelectSingleNode(WebConstants.TYPE_DATA).OuterHtml);
                    String priceString = dataNode.SelectSingleNode(WebConstants.LINK_HREF).InnerText.Trim();
                    int index = priceString.IndexOf('-');
                    if (index > 0)
                    {
                        String minPrice = priceString.Substring(0, index);
                        String maxPrice = priceString.Substring(index + 1, priceString.Length - 3 - minPrice.Length);
                        mCarTypeList[i].PriceMin = Convert.ToDouble(minPrice);
                        mCarTypeList[i].PriceMax = Convert.ToDouble(maxPrice);
                    } 
                    else
                    {
                        String price = priceString.Substring(0, priceString.Length - 2);
                        double carPrice;
                        if (double.TryParse(price, out carPrice))
                        {
                            mCarTypeList[i].PriceMin = mCarTypeList[i].PriceMax = carPrice;
                        }
                        else
                        {
                            mCarTypeList[i].PriceMin = mCarTypeList[i].PriceMax = 0;
                        }
                    }
                }
                mCarFactory.CarTypeList.AddRange(mCarTypeList);
            }
        }
    }
}
