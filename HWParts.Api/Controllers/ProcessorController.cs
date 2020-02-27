using HWParts.Core.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    public class ProcessorController : Controller
    {
        private readonly IProcessorRepository _processorRepository;

        public ProcessorController(IProcessorRepository processorRepository)
        {
            _processorRepository = processorRepository;
        }

        public async Task<IActionResult> List(int? page)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);

            var processors = await _processorRepository.PaginatedList(pageNumber, pageSize);
            return View(processors);
        }
    }
}
