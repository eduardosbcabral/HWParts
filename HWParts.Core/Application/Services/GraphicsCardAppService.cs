using AutoMapper;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Domain.Bus;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Repositories;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Services
{
    public class GraphicsCardAppService : IGraphicsCardAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler Bus;

        private readonly IGraphicsCardRepository _graphicsCardRepository;

        public GraphicsCardAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IGraphicsCardRepository graphicsCardRepository)
        {
            _mapper = mapper;
            Bus = bus;

            _graphicsCardRepository = graphicsCardRepository;
        }

        #region Commands
        public void Register(GraphicsCardViewModel graphicsCardViewModel)
        {
            var registerCommand = _mapper.Map<RegisterGraphicsCardCommand>(graphicsCardViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(GraphicsCardViewModel graphicsCardViewModel)
        {
            var updateCommand = _mapper.Map<UpdateGraphicsCardCommand>(graphicsCardViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveGraphicsCardCommand(id);
            Bus.SendCommand(removeCommand);
        }
        #endregion

        #region Queries
        public GraphicsCardViewModel GetById(Guid id)
        {
            var graphicsCard = _graphicsCardRepository.GetById(id);
            return _mapper.Map<GraphicsCardViewModel>(graphicsCard);
        }

        public PaginationObject<GraphicsCardViewModel> ListPaginated(int? page)
        {
            return _graphicsCardRepository.ListPaginated(page);
        }
        #endregion
    }
}
