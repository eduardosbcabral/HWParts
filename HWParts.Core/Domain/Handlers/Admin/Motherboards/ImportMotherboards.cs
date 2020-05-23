using HWParts.Core.Domain.Commands.Admin.Motherboards;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers.Admin
{
    public class ImportMotherboards : IRequestHandler<ImportMotherboardsCommand, bool>
    {
        private readonly HWPartsDbContext _context;

        public ImportMotherboards(HWPartsDbContext context) => _context = context;

        public async Task<bool> Handle(ImportMotherboardsCommand request, CancellationToken cancellationToken)
        {
            var motherboards = new List<Motherboard>();

            using (var reader = new StreamReader(request.File.OpenReadStream()))
            {
                var json = await reader.ReadToEndAsync();

                var processorsDeserialized = JsonConvert.DeserializeObject<List<dynamic>>(json);

                foreach (var item in processorsDeserialized)
                {
                    var brand = BindParameter<string>("Brand", item);
                    var model = BindParameter<string>("Model", item);
                    var cpuSocketType = BindParameter<string>("CPU Socket Type", item);
                    var cpuType = BindParameter<string>("CPU Type", item);
                    var chipset = BindParameter<string>("Chipset", item);
                    var numberOfMemorySlots = BindParameter<string>("Number of Memory Slots", item);
                    var memoryStandard = BindParameter<string>("Memory Standard", item);
                    var maximumMemorySupported = BindParameter<string>("Maximum Memory Supported", item);
                    var channelSupported = BindParameter<string>("Channel Supported", item);
                    var pciExpress30x16 = BindParameter<string>("PCI Express 3.0 x16", item);
                    var pciExpressx1 = BindParameter<string>("PCI Express x1", item);
                    var sata6Gbs = BindParameter<string>("SATA 6Gb/s", item);
                    var m2 = BindParameter<string>("M.2", item);
                    var onboardVideoChipset = BindParameter<string>("Onboard Video Chipset", item);
                    var audioChipset = BindParameter<string>("Audio Chipset", item);
                    var audioChannels = BindParameter<string>("Audio Channels", item);
                    var lanChipset = BindParameter<string>("LAN Chipset", item);
                    var maxLanSpeed = BindParameter<string>("Max LAN Speed", item);
                    var backIOPorts = BindParameter<string>("Back I/O Ports", item);
                    var onboardUsb = BindParameter<string>("Onboard USB", item);
                    var otherConnectors = BindParameter<string>("Other Connectors", item);
                    var formFactor = BindParameter<string>("Form Factor", item);
                    var dimensionsWxL = BindParameter<string>("Dimensions (W x L)", item);
                    var powerPin = BindParameter<string>("Power Pin", item);
                    var dateFirstAvailable = BindDateParameter("Date First Available", item, false);
                    var platformId = BindParameter<string>("platform_id", item);
                    var imageUrl = BindParameter<string>("image_url", item);
                    var url = BindParameter<string>("url", item);
                    var platform = (EPlatform)Enum.Parse(typeof(EPlatform), BindParameter<string>("platform", item), true);
                    var itemOrder = BindParameter<int>("item_order", item);
                    var crawledDate = BindDateParameter("crawled_date", item, true);

                    var motherboard = new Motherboard(
                        brand,
                        model,
                        platformId,
                        imageUrl,
                        url,
                        itemOrder,
                        platform);

                    var existsOnDb = _context.Motherboards
                            .Any(x => x.PlatformId == motherboard.PlatformId);

                    if (!existsOnDb)
                    {
                        var existsOnCurrentList = motherboards.Any(x => x.PlatformId == motherboard.PlatformId);

                        if (!existsOnCurrentList)
                        {
                            motherboards.Add(motherboard);
                        }
                    }
                    else
                    {
                        try
                        {
                            var motherboardFromDb = _context.Motherboards
                            .SingleOrDefault(x => x.PlatformId == motherboard.PlatformId);

                        }catch(Exception e)
                        {

                        }

                        // TODO: Implementar o update
                    }
                }
            }

            await _context.AddRangeAsync(motherboards);
            await _context.SaveChangesAsync();

            return true;
        }

        public T BindParameter<T>(string parameter, dynamic item)
        {
            return item[parameter] is null ? "" : item[parameter].ToObject<T>();
        }

        public DateTime? BindDateParameter(string parameter, dynamic item, bool culture)
        {
            if (culture)
            {
                return item[parameter] is null ? null : DateTime.ParseExact(item[parameter].ToObject<string>(), "dd/MM/yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("pt-BR"));
            }

            return item[parameter]?.ToObject<DateTime>();
        }
    }
}
