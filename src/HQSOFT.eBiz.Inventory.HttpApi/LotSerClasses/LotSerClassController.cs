using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using Volo.Abp.Content;
using HQSOFT.eBiz.Inventory.Shared;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    [RemoteService(Name = "Inventory")]
    [Area("inventory")]
    [ControllerName("LotSerClass")]
    [Route("api/inventory/lot-ser-classes")]
    public class LotSerClassController : AbpController, ILotSerClassesAppService
    {
        private readonly ILotSerClassesAppService _lotSerClassesAppService;

        public LotSerClassController(ILotSerClassesAppService lotSerClassesAppService)
        {
            _lotSerClassesAppService = lotSerClassesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<LotSerClassDto>> GetListAsync(GetLotSerClassesInput input)
        {
            return _lotSerClassesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<LotSerClassDto> GetAsync(Guid id)
        {
            return _lotSerClassesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<LotSerClassDto> CreateAsync(LotSerClassCreateDto input)
        {
            return _lotSerClassesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<LotSerClassDto> UpdateAsync(Guid id, LotSerClassUpdateDto input)
        {
            return _lotSerClassesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _lotSerClassesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerClassExcelDownloadDto input)
        {
            return _lotSerClassesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _lotSerClassesAppService.GetDownloadTokenAsync();
        }
    }
}