using HWParts.Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    public class MemoryController : Controller
    {
        private readonly IService<SyncronizeMemoriesByPlatform> _syncronizeMemories;

        public MemoryController(IService<SyncronizeMemoriesByPlatform> syncronizeMemories)
        {
            _syncronizeMemories = syncronizeMemories;
        }

        public async Task<IActionResult> Syncronize(bool check)
        {
            if (check)
            {
                await _syncronizeMemories.Execute();
                return Ok();
            }
            return BadRequest();
        }
    }
}
