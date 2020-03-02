using HWParts.Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    public class MotherboardController : Controller
    {
        private readonly IService<SyncronizeMotherboardsByPlatform> _syncronizeMotherboards;

        public MotherboardController(IService<SyncronizeMotherboardsByPlatform> syncronizeMotherboards)
        {
            _syncronizeMotherboards = syncronizeMotherboards;
        }

        public async Task<IActionResult> Syncronize(bool check)
        {
            if (check)
            {
                await _syncronizeMotherboards.Execute();
                return Ok();
            }
            return BadRequest();
        }
    }
}
