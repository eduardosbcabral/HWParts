using AngleSharp;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Queries;
using HWParts.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Services
{
    public class SyncronizeProcessorsByPlatform : IService<SyncronizeProcessorsByPlatform>
    {
        private readonly HWPartsDbContext _db;

        public SyncronizeProcessorsByPlatform(HWPartsDbContext db)
        {
            _db = db;
        }

        public async Task Execute()
        {
            var config = Configuration.Default
                .WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var url = @"https://www.newegg.com/Processors-Desktops/SubCategory/ID-343/Page-1?PageSize=96";

            var maximumPage = 0;

            using (var document = await context.OpenAsync(url))
            {
                var paginationElement = document.QuerySelector("span.list-tool-pagination-text");
                document.Close();
                var splitedPaginationElement = paginationElement.FirstElementChild.InnerHtml.Split('/');
                maximumPage = Convert.ToInt32(splitedPaginationElement[1]);
            }

            var processorsList = new List<Processor>();

            for (int i = 1; i <= maximumPage; i++)
            {
                url = $"https://www.newegg.com/Processors-Desktops/SubCategory/ID-343/Page-{i}?PageSize=96";
                using (var document = await context.OpenAsync(url))
                {
                    var processorsElementList = document.QuerySelectorAll(".items-view>.item-container");
                    document.Close();

                    foreach (var processorElement in processorsElementList)
                    {
                        var processorImageElement = processorElement.QuerySelector("a.item-img");
                        // Url
                        var processorUrl = processorImageElement.GetAttribute("href");

                        // Image Url
                        var imageElementTag = processorImageElement.Children.Where(x => x.TagName == "IMG").FirstOrDefault();
                        var processorImageUrl = imageElementTag.GetAttribute("src");

                        var processorUrlSplitted = processorUrl.Split('/');
                        // PlatformId
                        var processorPlatformId = processorUrlSplitted[processorUrlSplitted.Length - 1];

                        // Brand
                        var processorBrandElement = processorElement.QuerySelector("a.item-brand");
                        var processorBrandUrl = processorBrandElement.GetAttribute("href");
                        var splittedProcessorBrandUrl = processorBrandUrl.Split('/');
                        var processorBrand = splittedProcessorBrandUrl[3];


                        var processorFeaturesElementChildren = processorElement.QuerySelector("ul.item-features").Children;

                        // Series
                        var processorSeriesElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Series:")).FirstOrDefault();
                        var processorSeries = string.Empty;
                        if (processorSeriesElement != null)
                        {
                            var splittedProcessorSeriesContent = processorSeriesElement.InnerHtml.Split('>');
                            processorSeries = splittedProcessorSeriesContent[2].Remove(0, 1);
                        }

                        //L3 Cache
                        var processorL3CacheElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("L3 Cache:")).FirstOrDefault();
                        var processorL3Cache = string.Empty;
                        if (processorL3CacheElement != null)
                        {
                            var splittedProcessorL3CacheContent = processorL3CacheElement.InnerHtml.Split('>');
                            processorL3Cache = splittedProcessorL3CacheContent[2].Remove(0, 1);
                        }

                        //L2 Cache
                        var processorL2CacheElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("L2 Cache:")).FirstOrDefault();
                        var processorL2Cache = string.Empty;
                        if (processorL2CacheElement != null)
                        {
                            var splittedProcessorL2CacheContent = processorL2CacheElement.InnerHtml.Split('>');
                            processorL2Cache = splittedProcessorL2CacheContent[2].Remove(0, 1);
                        }

                        // Cooling Device
                        var processorCoolingDeviceElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Cooling Device:")).FirstOrDefault();
                        var processorCoolingDevice = string.Empty;
                        if (processorCoolingDeviceElement != null)
                        {
                            var splittedProcessorCoolingDeviceContent = processorCoolingDeviceElement.InnerHtml.Split('>');
                            processorCoolingDevice = splittedProcessorCoolingDeviceContent[2].Remove(0, 1);
                        }

                        // Manufacturing Tech
                        var processorManufacturingTechElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Manufacturing Tech:")).FirstOrDefault();
                        var processorManufacturingTech = string.Empty;
                        if (processorManufacturingTechElement != null)
                        {
                            var splittedProcessorManufacturingTechContent = processorManufacturingTechElement.InnerHtml.Split('>');
                            processorManufacturingTech = splittedProcessorManufacturingTechContent[2].Remove(0, 1);
                        }

                        // Model
                        var processorModelElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Model #:")).FirstOrDefault();
                        var processorModel = string.Empty;
                        if (processorModelElement != null)
                        {
                            var splittedProcessorModelContent = processorModelElement.InnerHtml.Split('>');
                            processorModel = string.IsNullOrEmpty(splittedProcessorModelContent[2]) ? splittedProcessorModelContent[2] : splittedProcessorModelContent[2].Remove(0, 1);
                        }

                        // Item
                        var processorItemElement = processorFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Item #:")).FirstOrDefault();
                        var processorItem = string.Empty;
                        if (processorItemElement != null)
                        {
                            var splittedProcessorItemContent = processorItemElement.InnerHtml.Split('>');
                            processorItem = splittedProcessorItemContent[2].Remove(0, 1);
                        }

                        // Price
                        var processorPriceElement = processorElement.QuerySelector("li.price-current");
                        var processorPrice = decimal.Zero;
                        if (processorPriceElement != null)
                        {
                            var firstPartPriceElement = processorPriceElement.Children.Where(x => x.TagName == "STRONG").FirstOrDefault();
                            var secondPartPriceElement = processorPriceElement.Children.Where(x => x.TagName == "SUP").FirstOrDefault();
                            if (firstPartPriceElement != null && secondPartPriceElement != null)
                            {
                                var priceContent = (firstPartPriceElement.InnerHtml + secondPartPriceElement.InnerHtml);
                                processorPrice = Convert.ToDecimal(priceContent, new CultureInfo("en-US"));
                            }
                        }

                        // Name
                        var processorNameElement = processorElement.QuerySelector("a.item-title");
                        var processorName = string.Empty;
                        if (processorNameElement != null)
                        {
                            processorName = processorNameElement.InnerHtml;
                        }


                        var processor = new Processor(
                            processorPlatformId,
                            processorName,
                            processorBrand,
                            processorSeries,
                            processorL3Cache,
                            processorL2Cache,
                            processorCoolingDevice,
                            processorManufacturingTech,
                            processorModel,
                            processorItem,
                            processorPrice,
                            processorImageUrl,
                            processorUrl);

                        var existsOnDb = _db.Processors
                            .Any(x => x.PlatformId == processor.PlatformId);

                        if (!existsOnDb)
                        {
                            var existsOnCurrentList = processorsList.Any(x => x.PlatformId == processor.PlatformId);

                            if (!existsOnCurrentList)
                            {
                                processorsList.Add(processor);
                            }
                        }
                        else
                        {
                            var processorFromDb = _db.Processors
                                .SingleOrDefault(x => x.PlatformId == processor.PlatformId);

                            processorFromDb.Update(
                                processorName,
                                processorBrand,
                                processorSeries,
                                processorL3Cache,
                                processorL2Cache,
                                processorCoolingDevice,
                                processorManufacturingTech,
                                processorModel,
                                processorItem,
                                processorPrice,
                                processorImageUrl,
                                processorUrl);
                        }
                    }

                    document.Dispose();
                }
                
                Thread.Sleep(5000);
            }

            await _db.AddRangeAsync(processorsList);
            await _db.SaveChangesAsync();
        }
    }
}
