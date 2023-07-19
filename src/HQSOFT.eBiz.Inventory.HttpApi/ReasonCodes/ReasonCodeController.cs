using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using Volo.Abp.Content;
using HQSOFT.eBiz.Inventory.Shared;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    [RemoteService(Name = "Inventory")]
    [Area("inventory")]
    [ControllerName("ReasonCode")]
    [Route("api/inventory/reason-codes")]
    public class ReasonCodeController : AbpController, IReasonCodesAppService
    {
        private readonly IReasonCodesAppService _reasonCodesAppService;

        public ReasonCodeController(IReasonCodesAppService reasonCodesAppService)
        {
            _reasonCodesAppService = reasonCodesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ReasonCodeDto>> GetListAsync(GetReasonCodesInput input)
        {
            return _reasonCodesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ReasonCodeDto> GetAsync(Guid id)
        {
            return _reasonCodesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ReasonCodeDto> CreateAsync(ReasonCodeUpdateDto input)
        {
            return _reasonCodesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ReasonCodeDto> UpdateAsync(Guid id, ReasonCodeUpdateDto input)
        {
            return _reasonCodesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _reasonCodesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ReasonCodeExcelDownloadDto input)
        {
            return _reasonCodesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _reasonCodesAppService.GetDownloadTokenAsync();
        }
    }
}