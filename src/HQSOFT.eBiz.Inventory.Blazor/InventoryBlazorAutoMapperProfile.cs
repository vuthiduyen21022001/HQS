using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using Volo.Abp.AutoMapper;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using AutoMapper;

namespace HQSOFT.eBiz.Inventory.Blazor;

public class InventoryBlazorAutoMapperProfile : Profile
{
    public InventoryBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<ReasonCodeDto, ReasonCodeUpdateDto>();

        CreateMap<LotSerClassDto, LotSerClassUpdateDto>();
        CreateMap<LotSerClassUpdateDto, LotSerClassCreateDto>();
        CreateMap<LotSerClassDto, LotSerClassCreateDto>();

        CreateMap<LotSerSegmentDto, LotSerSegmentUpdateDto>();
        CreateMap<LotSerSegmentDto, LotSerSegmentCreateDto>();
    }
}