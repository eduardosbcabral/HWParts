﻿using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure.Common.Pagination;
using System;

namespace HWParts.Core.Domain.Repositories
{
    public interface IGraphicsCardRepository : IRepository<GraphicsCard>
    {
        GraphicsCard GetByPlatformId(string platformId);
        PaginationObject<GraphicsCardViewModel> ListPaginated(int? page);
        bool Exists(Guid id);
    }
}
