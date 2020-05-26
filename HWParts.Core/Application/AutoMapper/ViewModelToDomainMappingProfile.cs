﻿using AutoMapper;
using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Commands;
using System;

namespace HWParts.Core.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MotherboardViewModel, RegisterMotherboardCommand>()
                .ConstructUsing(c => new RegisterMotherboardCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));

            CreateMap<MotherboardViewModel, UpdateMotherboardCommand>()
                .ConstructUsing(c => new UpdateMotherboardCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));

            CreateMap<GraphicsCardViewModel, RegisterGraphicsCardCommand>()
                .ConstructUsing(c => new RegisterGraphicsCardCommand(c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));

            CreateMap<GraphicsCardViewModel, UpdateGraphicsCardCommand>()
                .ConstructUsing(c => new UpdateGraphicsCardCommand(c.Id, c.PlatformId, c.ImageUrl, c.Url, c.Platform, c.Brand, c.Model));
        }
    }
}
