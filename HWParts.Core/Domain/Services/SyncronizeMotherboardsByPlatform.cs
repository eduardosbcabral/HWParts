using AngleSharp;
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
    public class SyncronizeMotherboardsByPlatform : IService<SyncronizeMotherboardsByPlatform>
    {
        private readonly HWPartsDbContext _db;

        public SyncronizeMotherboardsByPlatform(HWPartsDbContext db)
        {
            _db = db;
        }

        public async Task Execute()
        {
            var config = Configuration.Default
                .WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var amdMotherBoardList = new List<Motherboard>();

            amdMotherBoardList.AddRange(await GetAmdMotherboards(context));

            await _db.AddRangeAsync(amdMotherBoardList);
            await _db.SaveChangesAsync();

            var intelMotherBoardList = new List<Motherboard>();

            intelMotherBoardList.AddRange(await GetIntelMotherboards(context));

            await _db.AddRangeAsync(intelMotherBoardList);
            await _db.SaveChangesAsync();
        }

        private async Task<List<Motherboard>> GetAmdMotherboards(IBrowsingContext context)
        {
            var url = @"https://www.newegg.com/AMD-Motherboards/SubCategory/ID-22/Page-1?PageSize=96";
            return await GetMotherboards(url, context, "AMD");
        }

        private async Task<List<Motherboard>> GetIntelMotherboards(IBrowsingContext context)
        {
            var url = @"https://www.newegg.com/Intel-Motherboards/SubCategory/ID-280/Page-1?PageSize=96";
            return await GetMotherboards(url, context, "INTEL");
        }

        private async Task<List<Motherboard>> GetMotherboards(string url, IBrowsingContext context, string processorType)
        {
            var maximumPage = 1;

            var motherboardsList = new List<Motherboard>();

            for (int i = 1; i <= maximumPage; i++)
            {
                if(i != 1)
                {
                    var splittedUrl = url.Split('/');
                    var currentPage = splittedUrl[splittedUrl.Length - 1].Split('?')[0];
                    url = url.Replace(currentPage, $"Page-{i}");
                }

                using (var document = await context.OpenAsync(url))
                {
                    var paginationElement = document.QuerySelector("span.list-tool-pagination-text");
                    var splitedPaginationElement = paginationElement.FirstElementChild.InnerHtml.Split('/');
                    maximumPage = Convert.ToInt32(splitedPaginationElement[1]);

                    var motherboardsElementList = document.QuerySelectorAll(".items-view>.item-container");
                    document.Close();

                    foreach (var motherboardElement in motherboardsElementList)
                    {
                        var motherboardImageElement = motherboardElement.QuerySelector("a.item-img");
                        // Url
                        var motherboardUrl = motherboardImageElement.GetAttribute("href");

                        // Image Url
                        var imageElementTag = motherboardImageElement.Children.Where(x => x.TagName == "IMG").FirstOrDefault();
                        var motherboardImageUrl = imageElementTag.GetAttribute("src");

                        var motherboardUrlSplitted = motherboardUrl.Split('/');
                        // PlatformId
                        var motherboardPlatformId = motherboardUrlSplitted[motherboardUrlSplitted.Length - 1];

                        // Brand
                        var motherboardBrandElement = motherboardElement.QuerySelector("a.item-brand");
                        var motherboardBrand = string.Empty;
                        if(motherboardBrandElement != null)
                        {
                            var motherboardBrandUrl = motherboardBrandElement.GetAttribute("href");
                            if(motherboardBrand != null)
                            {
                                var splittedmotherboardBrandUrl = motherboardBrandUrl.Split('/');
                                motherboardBrand = splittedmotherboardBrandUrl[3];
                            }
                        }

                        var motherboardFeaturesElementChildren = motherboardElement.QuerySelector("ul.item-features").Children;

                        // Number of Memory Slots
                        var motherboardNumberOfMemorySlotsElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Number of Memory Slots:")).FirstOrDefault();
                        var motherboardNumberOfMemorySlots = string.Empty;
                        if (motherboardNumberOfMemorySlotsElement != null)
                        {
                            var splittedmotherboardNumberOfMemorySlotsContent = motherboardNumberOfMemorySlotsElement.InnerHtml.Split('>');
                            motherboardNumberOfMemorySlots = splittedmotherboardNumberOfMemorySlotsContent[2].Remove(0, 1);
                        }

                        // Memory Standard
                        var motherboardMemoryStandardElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Memory Standard:")).FirstOrDefault();
                        var motherboardMemoryStandard = string.Empty;
                        if (motherboardMemoryStandardElement != null)
                        {
                            var splittedmotherboardMemoryStandardContent = motherboardMemoryStandardElement.InnerHtml.Split('>');
                            motherboardMemoryStandard = splittedmotherboardMemoryStandardContent[2].Remove(0, 1);
                        }

                        // Onboard Video Chipset
                        var motherboardOnboardVideoChipsetElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Onboard Video Chipset:")).FirstOrDefault();
                        var motherboardOnboardVideoChipset = string.Empty;
                        if (motherboardOnboardVideoChipsetElement != null)
                        {
                            var splittedMotherboardOnboardVideoChipsetContent = motherboardOnboardVideoChipsetElement.InnerHtml.Split('>');
                            motherboardOnboardVideoChipset = splittedMotherboardOnboardVideoChipsetContent[2].Remove(0, 1);
                        }

                        // Audio Chipset
                        var motherboardAudioChipsetElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Audio Chipset:")).FirstOrDefault();
                        var motherboardAudioChipset = string.Empty;
                        if (motherboardAudioChipsetElement != null)
                        {
                            var splittedMotherboardAudioChipsetContent = motherboardAudioChipsetElement.InnerHtml.Split('>');
                            motherboardAudioChipset = splittedMotherboardAudioChipsetContent[2].Remove(0, 1);
                        }

                        // Audio Channels
                        var motherboardAudioChannelsElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Audio Channels:")).FirstOrDefault();
                        var motherboardAudioChannels = string.Empty;
                        if (motherboardAudioChannelsElement != null)
                        {
                            var splittedmotherboardAudioChannelsContent = motherboardAudioChannelsElement.InnerHtml.Split('>');
                            motherboardAudioChannels = splittedmotherboardAudioChannelsContent[2].Remove(0, 1);
                        }

                        // Model
                        var motherboardModelElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Model #:")).FirstOrDefault();
                        var motherboardModel = string.Empty;
                        if (motherboardModelElement != null)
                        {
                            var splittedmotherboardModelContent = motherboardModelElement.InnerHtml.Split('>');
                            motherboardModel = string.IsNullOrEmpty(splittedmotherboardModelContent[2]) ? splittedmotherboardModelContent[2] : splittedmotherboardModelContent[2].Remove(0, 1);
                        }

                        // Item
                        var motherboardItemElement = motherboardFeaturesElementChildren.Where(x => x.InnerHtml.Contains("Item #:")).FirstOrDefault();
                        var motherboardItem = string.Empty;
                        if (motherboardItemElement != null)
                        {
                            var splittedmotherboardItemContent = motherboardItemElement.InnerHtml.Split('>');
                            motherboardItem = splittedmotherboardItemContent[2].Remove(0, 1);
                        }

                        // Price
                        var motherboardPriceElement = motherboardElement.QuerySelector("li.price-current");
                        var motherboardPrice = decimal.Zero;
                        if (motherboardPriceElement != null)
                        {
                            var firstPartPriceElement = motherboardPriceElement.Children.Where(x => x.TagName == "STRONG").FirstOrDefault();
                            var secondPartPriceElement = motherboardPriceElement.Children.Where(x => x.TagName == "SUP").FirstOrDefault();
                            if (firstPartPriceElement != null && secondPartPriceElement != null)
                            {
                                var priceContent = (firstPartPriceElement.InnerHtml + secondPartPriceElement.InnerHtml);
                                motherboardPrice = Convert.ToDecimal(priceContent, new CultureInfo("en-US"));
                            }
                        }

                        // Name
                        var motherboardNameElement = motherboardElement.QuerySelector("a.item-title");
                        var motherboardName = string.Empty;
                        if (motherboardNameElement != null)
                        {
                            motherboardName = motherboardNameElement.InnerHtml;
                        }


                        var motherboard = new Motherboard(
                            motherboardPlatformId,
                            motherboardName,
                            motherboardBrand,
                            processorType,
                            motherboardNumberOfMemorySlots,
                            motherboardMemoryStandard,
                            motherboardOnboardVideoChipset,
                            motherboardAudioChipset,
                            motherboardAudioChannels,
                            motherboardModel,
                            motherboardItem,
                            motherboardPrice,
                            motherboardImageUrl,
                            motherboardUrl);

                        var existsOnDb = _db.Motherboards
                            .Any(x => x.PlatformId == motherboard.PlatformId);

                        if (!existsOnDb)
                        {
                            var existsOnCurrentList = motherboardsList.Any(x => x.PlatformId == motherboard.PlatformId);

                            if (!existsOnCurrentList)
                            {
                                motherboardsList.Add(motherboard);
                            }
                        }
                        else
                        {
                            var motherboardFromDb = _db.Motherboards
                                .SingleOrDefault(x => x.PlatformId == motherboard.PlatformId);

                            motherboardFromDb.Update(
                                motherboardPlatformId,
                                motherboardName,
                                motherboardBrand,
                                motherboardNumberOfMemorySlots,
                                motherboardMemoryStandard,
                                motherboardOnboardVideoChipset,
                                motherboardAudioChipset,
                                motherboardAudioChannels,
                                motherboardModel,
                                motherboardItem,
                                motherboardPrice,
                                motherboardImageUrl,
                                motherboardUrl);
                        }
                    }

                    document.Dispose();
                }

                Thread.Sleep(5000);
            }

            return motherboardsList;
        }
    }
}
