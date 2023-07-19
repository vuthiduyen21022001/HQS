using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HQSOFT.eBiz.Inventory.Shared;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public interface ILotSerClassesAppService : IApplicationService
    {
        Task<PagedResultDto<LotSerClassDto>> GetListAsync(GetLotSerClassesInput input);

        Task<LotSerClassDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<LotSerClassDto> CreateAsync(LotSerClassCreateDto input);

        Task<LotSerClassDto> UpdateAsync(Guid id, LotSerClassUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerClassExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}