using AutoMapper;
using HWParts.Core.Domain.Commands.Admin.Motherboards;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Repositories;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IMotherboardRepository _motherboardRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminController(
            IMotherboardRepository motherboardRepository,
            IMediator mediator,
            IMapper mapper)
        {
            _motherboardRepository = motherboardRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Motherboards")]
        public async Task<IActionResult> Motherboards(int? page)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);

            var motherboards = await _motherboardRepository.PaginatedList(pageNumber, pageSize);
            return View(motherboards);
        }

        [HttpGet("Motherboards/Import")]
        public IActionResult ImportMotherboardsView()
        {
            return View("ImportMotherboards");
        }

        [HttpPost("Motherboards/Import")]
        public async Task<IActionResult> ImportMotherboards(ImportMotherboardsCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("ImportMotherboards", command);
            }

            var response = await _mediator.Send(command);
            return RedirectToAction(nameof(Motherboards));
        }

        [HttpGet("Motherboards/Edit/{id}")]
        public async Task<IActionResult> EditMotherboardView(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity = await _motherboardRepository.GetByIdAsync(id);
            var motherboard = _mapper.Map<Motherboard, EditMotherboardViewModel>(entity);
            return View("EditMotherboard", motherboard);
        }

        [HttpPost("Motherboards/Edit/{id}")]
        public async Task<IActionResult> EditMotherboard(string id, EditMotherboardViewModel motherboard)
        {
            if (id != motherboard.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("EditMotherboard", motherboard);
            }

            var command = _mapper.Map<EditMotherboardViewModel, EditMotherboardCommand>(motherboard);

            var result = await _mediator.Send(command);

            ViewBag.Edited = true;

            return View("EditMotherboard", result);
        }

        [HttpGet("Motherboards/Create")]
        public IActionResult CreateMotherboardView()
        {
            return View("CreateMotherboard", new CreateMotherboardViewModel());
        }

        [HttpPost("Motherboards/Create")]
        public async Task<IActionResult> CreateMotherboard(CreateMotherboardViewModel motherboard)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateMotherboard", motherboard);
            }

            var command = _mapper.Map<CreateMotherboardViewModel, CreateMotherboardCommand>(motherboard);

            var result = await _mediator.Send(command);

            ViewBag.Edited = true;

            return View("CreateMotherboard", result);
        }
    }
}
