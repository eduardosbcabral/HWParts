using HWParts.Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    public class GraphicsCardController : Controller
    {
        private readonly IService<SyncronizeGraphicsCardsByPlatform> _syncronizeGraphicsCards;

        public GraphicsCardController(IService<SyncronizeGraphicsCardsByPlatform> syncronizeGraphicsCards)
        {
            _syncronizeGraphicsCards = syncronizeGraphicsCards;
        }

        public async Task<IActionResult> Syncronize(bool check)
        {
            if (check)
            {
                await _syncronizeGraphicsCards.Execute();
                return Ok();
            }
            return BadRequest();
        }
    }
}
