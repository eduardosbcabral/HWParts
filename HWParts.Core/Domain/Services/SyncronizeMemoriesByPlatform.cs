using AngleSharp;
using AngleSharp.Dom;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Services
{
    public class SyncronizeMemoriesByPlatform : IService<SyncronizeMemoriesByPlatform>
    {
        private readonly HWPartsDbContext _context;

        public SyncronizeMemoriesByPlatform(HWPartsDbContext context)
        {
            _context = context;
        }

        public async Task Execute()
        {
            var config = Configuration.Default
                .WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var maximumPage = 1;
            var memoryOrder = 0;

            var memoryList = new List<Memory>();

            for (int i = 1; i <= maximumPage; i++)
            {
                var url = $"https://www.newegg.com/Desktop-Memory/SubCategory/ID-147/Page-{i}?PageSize=96";

                IHtmlCollection<IElement> memoriesElementList = null;

                using (var document = await context.OpenAsync(url))
                {
                    maximumPage = GetMaximumPage(document);
                    memoriesElementList = GetMemoriesList(document);
                    document.Close();
                }

                foreach (var memoryElement in memoriesElementList)
                {
                    // Order
                    memoryOrder++;

                    // Url
                    var productUrl = GetUrl(memoryElement);

                    // Image Url
                    var imageUrl = GetImageUrl(memoryElement);

                    // PlatformId
                    var platformId = GetPlatformId(productUrl);

                    // Brand
                    var brand = GetBrand(memoryElement);

                    // CAS Latency
                    var casLatency = GetElementContent("CAS Latency:", memoryElement);

                    // Voltage
                    var voltage = GetElementContent("Voltage:", memoryElement);

                    // Multi-Channel Kit
                    var multiChannelKit = GetElementContent("Multi-channel Kit:", memoryElement);

                    // Timing
                    var timing = GetElementContent("Timing:", memoryElement);

                    // Heat Spreader
                    var heatSpreader = GetElementContent("Heat Spreader:", memoryElement);

                    // Model
                    var model = GetElementContent("Model #:", memoryElement);

                    // Item
                    var item = GetElementContent("Model #:", memoryElement);

                    // Price
                    var price = GetPrice(memoryElement);

                    // Name
                    var name = GetName(memoryElement);


                    var graphicsCard = new Memory(
                        platformId,
                        name,
                        brand,
                        memoryOrder,
                        casLatency,
                        voltage,
                        multiChannelKit,
                        timing,
                        heatSpreader,
                        model,
                        item,
                        price,
                        imageUrl,
                        productUrl);

                    var existsOnDb = _context.GraphicsCards
                        .Any(x => x.PlatformId == graphicsCard.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = memoryList
                            .Any(x => x.PlatformId == graphicsCard.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            memoryList.Add(graphicsCard);
                        }
                    }
                    else
                    {
                        var memoryFromDb = _context.Memories
                            .SingleOrDefault(x => x.PlatformId == graphicsCard.PlatformId);

                        memoryFromDb.Update(
                            platformId,
                            name,
                            brand,
                            memoryOrder,
                            casLatency,
                            voltage,
                            multiChannelKit,
                            timing,
                            heatSpreader,
                            model,
                            item,
                            price,
                            imageUrl,
                            productUrl);
                    }
                }

                Thread.Sleep(5000);
            }

            await _context.AddRangeAsync(memoryList);
            await _context.SaveChangesAsync();
        }

        private int GetMaximumPage(IDocument document)
        {
            var paginationElement = document.QuerySelector("span.list-tool-pagination-text");
            var splitedPaginationElement = paginationElement.FirstElementChild.InnerHtml.Split('/');
            var page = splitedPaginationElement
                .Where((_, i) => i == 1)
                .First();
            return Convert.ToInt32(page);
        }

        private IHtmlCollection<IElement> GetMemoriesList(IDocument document)
        {
            return document.QuerySelectorAll(".items-view>.item-container");
        }
        private string GetUrl(IElement element)
        {
            var imageElement = element.QuerySelector("a.item-img");
            return imageElement.GetAttribute("href");
        }

        private string GetImageUrl(IElement element)
        {
            var imageElement = element.QuerySelector("a.item-img");
            var imageElementTag = imageElement.Children
                .FirstOrDefault(x => x.TagName == "IMG");
            return imageElementTag.GetAttribute("src");
        }

        private string GetPlatformId(string graphicsCardUrl)
        {
            var urlSplitted = graphicsCardUrl.Split('/');
            return urlSplitted
                .Where((_, i) => i == (urlSplitted.Length - 1))
                .First();
        }

        private string GetBrand(IElement element)
        {
            var brandElement = element.QuerySelector("a.item-brand");
            if (brandElement is null)
            {
                return string.Empty;
            }

            var brandUrl = brandElement.GetAttribute("href");
            var splittedBrandUrl = brandUrl.Split('/');
            return splittedBrandUrl
                .Where((_, i) => i == 3)
                .First();
        }

        private decimal GetPrice(IElement element)
        {
            var priceElement = element.QuerySelector("li.price-current");
            if (priceElement is null)
            {
                return decimal.Zero;
            }

            var firstPartPriceElement = priceElement.Children.Where(x => x.TagName == "STRONG").FirstOrDefault();
            var secondPartPriceElement = priceElement.Children.Where(x => x.TagName == "SUP").FirstOrDefault();
            if (firstPartPriceElement is null && secondPartPriceElement is null)
            {
                return decimal.Zero;
            }

            var priceContent = (firstPartPriceElement.InnerHtml + secondPartPriceElement.InnerHtml);
            return Convert.ToDecimal(priceContent, new CultureInfo("en-US"));
        }

        private string GetName(IElement element)
        {
            var nameElement = element.QuerySelector("a.item-title");
            if (nameElement is null)
            {
                return string.Empty;
            }

            return nameElement.InnerHtml;
        }

        private IHtmlCollection<IElement> GetFeaturesElement(IElement element)
        {
            return element.QuerySelector("ul.item-features").Children;
        }

        private string GetElementContent(string featureTitle, IElement element)
        {
            var featureElement = GetFeaturesElement(element)
                .FirstOrDefault(x => x.InnerHtml.Contains(featureTitle));

            if (featureElement is null)
            {
                return string.Empty;
            }

            var splittedContent = featureElement.InnerHtml.Split('>');

            var content = splittedContent
                .Where((_, i) => i == 2)
                .First();

            return string.IsNullOrEmpty(content) ? content : content.Remove(0, 1);
        }
    }
}
