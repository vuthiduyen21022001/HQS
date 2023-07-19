using HQSOFT.eBiz.Inventory.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public interface ILotSerSegmentsAppService : IApplicationService
    {
        Task<PagedResultDto<LotSerSegmentWithNavigationPropertiesDto>> GetListAsync(GetLotSerSegmentsInput input);

        Task<LotSerSegmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LotSerSegmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetLotSerClassLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<LotSerSegmentDto> CreateAsync(LotSerSegmentCreateDto input);

        Task<LotSerSegmentDto> UpdateAsync(Guid id, LotSerSegmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerSegmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
        Task<List<LotSerSegmentDto>> GetListAllClassDetail(Guid id);
    }
}