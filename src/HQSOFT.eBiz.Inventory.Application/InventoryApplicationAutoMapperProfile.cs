using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using HQSOFT.eBiz.Inventory.Shared;
using Volo.Abp.AutoMapper;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using AutoMapper;

namespace HQSOFT.eBiz.Inventory;

public class InventoryApplicationAutoMapperProfile : Profile
{
    public InventoryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<ReasonCode, ReasonCodeDto>();
        CreateMap<ReasonCode, ReasonCodeExcelDto>();

        CreateMap<LotSerClass, LotSerClassDto>();
        CreateMap<LotSerClass, LotSerClassExcelDto>();

        CreateMap<LotSerSegment, LotSerSegmentDto>();
        CreateMap<LotSerSegment, LotSerSegmentExcelDto>();

        CreateMap<LotSerSegmentWithNavigationProperties, LotSerSegmentWithNavigationPropertiesDto>();
        CreateMap<LotSerClass, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ClassID));
    }
}