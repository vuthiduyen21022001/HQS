using HQSOFT.eBiz.Inventory.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.Inventory.LotSerSegments;
using Volo.Abp.Content;
using HQSOFT.eBiz.Inventory.Shared;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    [RemoteService(Name = "Inventory")]
    [Area("inventory")]
    [ControllerName("LotSerSegment")]
    [Route("api/inventory/lot-ser-segments")]
    public class LotSerSegmentController : AbpController, ILotSerSegmentsAppService
    {
        private readonly ILotSerSegmentsAppService _lotSerSegmentsAppService;

        public LotSerSegmentController(ILotSerSegmentsAppService lotSerSegmentsAppService)
        {
            _lotSerSegmentsAppService = lotSerSegmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<LotSerSegmentWithNavigationPropertiesDto>> GetListAsync(GetLotSerSegmentsInput input)
        {
            return _lotSerSegmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<LotSerSegmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _lotSerSegmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<LotSerSegmentDto> GetAsync(Guid id)
        {
            return _lotSerSegmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("lot-ser-class-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetLotSerClassLookupAsync(LookupRequestDto input)
        {
            return _lotSerSegmentsAppService.GetLotSerClassLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<LotSerSegmentDto> CreateAsync(LotSerSegmentCreateDto input)
        {
            return _lotSerSegmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<LotSerSegmentDto> UpdateAsync(Guid id, LotSerSegmentUpdateDto input)
        {
            return _lotSerSegmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _lotSerSegmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerSegmentExcelDownloadDto input)
        {
            return _lotSerSegmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _lotSerSegmentsAppService.GetDownloadTokenAsync();
        }
        [HttpGet]
        [Route("list-detail/{id}")]
        public Task<List<LotSerSegmentDto>> GetListAllClassDetail(Guid id)
        {
            return _lotSerSegmentsAppService.GetListAllClassDetail(id);
        }


    }
}