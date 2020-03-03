using AngleSharp;
using AngleSharp.Dom;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Services
{
    public class SyncronizeGraphicsCardsByPlatform : IService<SyncronizeGraphicsCardsByPlatform>
    {
        private readonly HWPartsDbContext _context;

        public SyncronizeGraphicsCardsByPlatform(HWPartsDbContext context)
        {
            _context = context;
        }

        public async Task Execute()
        {
            var config = Configuration.Default
                .WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var maximumPage = 1;
            var graphicsCardOrder = 0;

            var graphicsCardList = new List<GraphicsCard>();

            for (int i = 1; i <= maximumPage; i++)
            {
                var url = $"https://www.newegg.com/Desktop-Graphics-Cards/SubCategory/ID-48/Page-{i}?PageSize=96";

                IHtmlCollection<IElement> graphicsCardsElementList = null;

                using (var document = await context.OpenAsync(url))
                {
                    maximumPage = GetMaximumPage(document);
                    graphicsCardsElementList = GetGraphicsCardsList(document);
                    document.Close();
                }

                foreach (var graphicsCardElement in graphicsCardsElementList)
                {
                    // Order
                    graphicsCardOrder++;

                    // Url
                    var graphicsCardUrl = GetUrl(graphicsCardElement);

                    // Image Url
                    var imageUrl = GetImageUrl(graphicsCardElement);

                    // PlatformId
                    var platformId = GetPlatformId(graphicsCardUrl);

                    // Brand
                    var brand = GetBrand(graphicsCardElement);

                    // Chipset Manufacturer
                    var chipsetManufacturer = GetChipsetManufacturer(graphicsCardElement);

                    // Core Clock
                    var coreClock = GetCoreClock(graphicsCardElement);

                    // Max Resolution
                    var maxResolution = GetMaxResolution(graphicsCardElement);

                    // Display Port
                    var displayPort = GetDisplayPort(graphicsCardElement);

                    // HDMI
                    var hdmi = GetHDMI(graphicsCardElement);

                    // DVI
                    var dvi = GetDVI(graphicsCardElement);

                    // Card Dimensions
                    var cardDimensions = GetCardDimensions(graphicsCardElement);

                    // Model
                    var model = GetModel(graphicsCardElement);

                    // Item
                    var item = GetItem(graphicsCardElement);

                    // Price
                    var price = GetPrice(graphicsCardElement);

                    // Name
                    var name = GetName(graphicsCardElement);


                    var graphicsCard = new GraphicsCard(
                        platformId,
                        name,
                        brand,
                        graphicsCardOrder,
                        chipsetManufacturer,
                        coreClock,
                        maxResolution,
                        displayPort,
                        hdmi,
                        dvi,
                        cardDimensions,
                        model,
                        item,
                        price,
                        imageUrl,
                        graphicsCardUrl);

                    var existsOnDb = _context.GraphicsCards
                        .Any(x => x.PlatformId == graphicsCard.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = graphicsCardList
                            .Any(x => x.PlatformId == graphicsCard.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            graphicsCardList.Add(graphicsCard);
                        }
                    }
                    else
                    {
                        var graphicsCardFromDb = _context.GraphicsCards
                            .SingleOrDefault(x => x.PlatformId == graphicsCard.PlatformId);

                        graphicsCardFromDb.Update(
                            platformId,
                            name,
                            brand,
                            graphicsCardOrder,
                            chipsetManufacturer,
                            coreClock,
                            maxResolution,
                            displayPort,
                            hdmi,
                            dvi,
                            cardDimensions,
                            model,
                            item,
                            price,
                            imageUrl,
                            graphicsCardUrl);
                    }
                }

                Thread.Sleep(5000);
            }

            await _context.AddRangeAsync(graphicsCardList);
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

        private IHtmlCollection<IElement> GetGraphicsCardsList(IDocument document)
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
            if(brandElement is null)
            {
                return string.Empty;
            }

            var brandUrl = brandElement.GetAttribute("href");
            var splittedBrandUrl = brandUrl.Split('/');
            return splittedBrandUrl
                .Where((_, i) => i == 3)
                .First();
        }

        private string GetChipsetManufacturer(IElement element)
        {
            return GetElementContent("Chipset Manufacturer:", element);
        }

        private string GetCoreClock(IElement element)
        {
            return GetElementContent("Core Clock:", element);
        }

        private string GetMaxResolution(IElement element) 
        {
            return GetElementContent("Max Resolution:", element);

        }

        private string GetDisplayPort(IElement element)
        {
            return GetElementContent("DisplayPort:", element);
        }

        private string GetHDMI(IElement element)
        {
            return GetElementContent("HDMI:", element);
        }

        private string GetDVI(IElement element)
        {
            return GetElementContent("DVI:", element);
        }

        private string GetCardDimensions(IElement element)
        {
            return GetElementContent("Card Dimensions (L x H):", element);
        }

        private string GetModel(IElement element)
        {
            return GetElementContent("Model #:", element);
        }

        private string GetItem(IElement element)
        {
            return GetElementContent("Item #:", element);
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
