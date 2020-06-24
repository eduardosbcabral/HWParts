﻿using HWParts.Core.Application.ViewModels.ComponentPrice;
using System;
using System.Collections.Generic;

namespace HWParts.Core.Application.Interfaces
{
    public interface IComponentPriceAppService
    {
        IList<ComponentPriceViewModel> GetAllPricesByComponentId(Guid componentId);
    }
}
