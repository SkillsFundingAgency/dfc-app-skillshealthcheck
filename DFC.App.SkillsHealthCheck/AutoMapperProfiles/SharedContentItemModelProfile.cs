﻿using System.Diagnostics.CodeAnalysis;

using AutoMapper;

using DFC.App.SkillsHealthCheck.Data.Models.CmsApiModels;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels;

using Microsoft.AspNetCore.Html;

namespace DFC.App.SkillsHealthCheck.AutoMapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class SharedContentItemModelProfile : Profile
    {
        public SharedContentItemModelProfile()
        {
            CreateMap<SharedContentItemApiDataModel, SharedContentItemModel>()
                .ForMember(d => d.Id, s => s.MapFrom(a => a.ItemId))
                .ForMember(d => d.Etag, s => s.Ignore())
                .ForMember(d => d.ParentId, s => s.Ignore())
                .ForMember(d => d.TraceId, s => s.Ignore())
                .ForMember(d => d.PartitionKey, s => s.Ignore())
                .ForMember(d => d.LastReviewed, s => s.MapFrom(a => a.Published))
                .ForMember(d => d.LastCached, s => s.Ignore());

            CreateMap<SharedContentItemModel, IndexDocumentViewModel>();

            CreateMap<SharedContentItemModel, BaseDocumentViewModel>()
                .ForMember(d => d.Head, s => s.MapFrom(a => a))
                .ForMember(d => d.Breadcrumb, s => s.Ignore())
                .ForMember(d => d.BodyViewModel, s => s.MapFrom(a => a));

            CreateMap<SharedContentItemModel, HeadViewModel>()
                .ForMember(d => d.CanonicalUrl, s => s.Ignore())
                .ForMember(d => d.Description, s => s.Ignore())
                .ForMember(d => d.Keywords, s => s.Ignore());

            CreateMap<SharedContentItemModel, BaseBodyViewModel>()
                .ForMember(d => d.Body, s => s.MapFrom(a => new HtmlString(a.Content)));

            CreateMap<SharedContentItemModel, BreadcrumbItemModel>()
                .ForMember(d => d.Route, s => s.Ignore());

            CreateMap<BreadcrumbItemModel, BreadcrumbItemViewModel>()
                .ForMember(d => d.AddHyperlink, s => s.Ignore());
        }
    }
}
