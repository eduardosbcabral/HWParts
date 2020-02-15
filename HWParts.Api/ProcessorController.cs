using AngleSharp;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Services;
using HWParts.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    [ApiController]
    [Route("/api/processors")]
    public class ProcessorController : ControllerBase
    {
        private readonly HWPartsDbContext _db;
        private readonly IService<SyncronizeProcessorsByPlatform> _syncronizeProcessorsByPlatform;

        public ProcessorController(
            HWPartsDbContext db,
            IService<SyncronizeProcessorsByPlatform> syncronizeProcessorsByPlatform)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _syncronizeProcessorsByPlatform = syncronizeProcessorsByPlatform;
        }

        [HttpGet]
        public async Task SaveProcessors()
        {
            await _syncronizeProcessorsByPlatform.Execute();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IList<Processor>> GetAll()
        {
            var processors = _db.Set<Processor>().AsQueryable();
            return processors.ToList();
        }

        private async Task<IList<Processor>> GetProcessors()
        {
            var config = Configuration.Default
                .WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var url = @"https://www.newegg.com/Processors-Desktops/SubCategory/ID-343";
            //var url = @"https://www.google.com.br";

            var document = await context.OpenAsync(url);

            var processorsElementList = document.QuerySelectorAll(".items-view>.item-container");

            var processorsList = new List<Processor>();

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


                var processorFeaturesElement = processorElement.QuerySelector("ul.item-features");

                // Series
                var processorSeriesElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("Series:")).FirstOrDefault();
                var processorSeries = string.Empty;
                if (processorSeriesElement != null)
                {
                    var splittedProcessorSeriesContent = processorSeriesElement.InnerHtml.Split(">");
                    processorSeries = splittedProcessorSeriesContent[2].Remove(0, 1);
                }

                //L3 Cache
                var processorL3CacheElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("L3 Cache:")).FirstOrDefault();
                var processorL3Cache = string.Empty;
                if (processorL3CacheElement != null)
                {
                    var splittedProcessorL3CacheContent = processorL3CacheElement.InnerHtml.Split(">");
                    processorL3Cache = splittedProcessorL3CacheContent[2].Remove(0, 1);
                }

                //L2 Cache
                var processorL2CacheElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("L2 Cache:")).FirstOrDefault();
                var processorL2Cache = string.Empty;
                if (processorL2CacheElement != null)
                {
                    var splittedProcessorL2CacheContent = processorL2CacheElement.InnerHtml.Split(">");
                    processorL2Cache = splittedProcessorL2CacheContent[2].Remove(0, 1);
                }

                // Cooling Device
                var processorCoolingDeviceElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("Cooling Device:")).FirstOrDefault();
                var processorCoolingDevice = string.Empty;
                if (processorCoolingDeviceElement != null)
                {
                    var splittedProcessorCoolingDeviceContent = processorCoolingDeviceElement.InnerHtml.Split(">");
                    processorCoolingDevice = splittedProcessorCoolingDeviceContent[2].Remove(0, 1);
                }

                // Manufacturing Tech
                var processorManufacturingTechElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("Manufacturing Tech:")).FirstOrDefault();
                var processorManufacturingTech = string.Empty;
                if (processorManufacturingTechElement != null)
                {
                    var splittedProcessorManufacturingTechContent = processorManufacturingTechElement.InnerHtml.Split(">");
                    processorManufacturingTech = splittedProcessorManufacturingTechContent[2].Remove(0, 1);
                }

                // Model
                var processorModelElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("Model #:")).FirstOrDefault();
                var processorModel = string.Empty;
                if (processorModelElement != null)
                {
                    var splittedProcessorModelContent = processorModelElement.InnerHtml.Split(">");
                    processorModel = string.IsNullOrEmpty(splittedProcessorModelContent[2]) ? splittedProcessorModelContent[2] : splittedProcessorModelContent[2].Remove(0, 1);
                }

                // Item
                var processorItemElement = processorFeaturesElement.Children.Where(x => x.InnerHtml.Contains("Item #:")).FirstOrDefault();
                var processorItem = string.Empty;
                if (processorItemElement != null)
                {
                    var splittedProcessorItemContent = processorItemElement.InnerHtml.Split(">");
                    processorItem = splittedProcessorItemContent[2].Remove(0, 1);
                }

                // Price
                var processorPriceElement = processorElement.QuerySelector("li.price-current");
                var firstPartPriceElement = processorPriceElement.Children.Where(x => x.TagName == "STRONG").FirstOrDefault();
                var secondPartPriceElement = processorPriceElement.Children.Where(x => x.TagName == "SUP").FirstOrDefault();
                var priceContent = (firstPartPriceElement.InnerHtml + secondPartPriceElement.InnerHtml);
                var processorPrice = Convert.ToDecimal(priceContent, new CultureInfo("en-US"));

                // Name
                var processorNameElement = processorElement.QuerySelector("a.item-title");
                var processorName = processorNameElement.InnerHtml;


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

                processorsList.Add(processor);
            }

            return processorsList;
        }
    }
}
