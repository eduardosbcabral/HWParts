using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Application.Interfaces
{
    public interface IGraphicsCardAppService
    {
        void Register(GraphicsCardViewModel graphicsCardViewModel);
        void Update(GraphicsCardViewModel graphicsCardViewModel);
        void Remove(Guid id);

        GraphicsCardViewModel GetById(Guid id);
        PaginationObject<GraphicsCardViewModel> ListPaginated(int? page);
    }
}
