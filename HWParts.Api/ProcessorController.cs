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
    }
}
