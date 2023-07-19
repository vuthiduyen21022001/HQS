using HQSOFT.eBiz.Inventory.Shared;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HQSOFT.eBiz.Inventory.Permissions;
using HQSOFT.eBiz.Inventory.LotSerSegments;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.eBiz.Inventory.Shared;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{

    [Authorize(InventoryPermissions.LotSerSegments.Default)]
    public class LotSerSegmentsAppService : ApplicationService, ILotSerSegmentsAppService
    {
        private readonly IDistributedCache<LotSerSegmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;
        private readonly LotSerSegmentManager _lotSerSegmentManager;
        private readonly IRepository<LotSerClass, Guid> _lotSerClassRepository;


        public LotSerSegmentsAppService(ILotSerSegmentRepository lotSerSegmentRepository, LotSerSegmentManager lotSerSegmentManager, IDistributedCache<LotSerSegmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<LotSerClass, Guid> lotSerClassRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _lotSerSegmentRepository = lotSerSegmentRepository;
            _lotSerSegmentManager = lotSerSegmentManager; _lotSerClassRepository = lotSerClassRepository;
        }

        public virtual async Task<PagedResultDto<LotSerSegmentWithNavigationPropertiesDto>> GetListAsync(GetLotSerSegmentsInput input)
        {
            var totalCount = await _lotSerSegmentRepository.GetCountAsync(input.FilterText, input.SegmentIDMin, input.SegmentIDMax, input.AsgmentType, input.Value, input.LotSerClassId);
            var items = await _lotSerSegmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.SegmentIDMin, input.SegmentIDMax, input.AsgmentType, input.Value, input.LotSerClassId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<LotSerSegmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<LotSerSegmentWithNavigationProperties>, List<LotSerSegmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<LotSerSegmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<LotSerSegmentWithNavigationProperties, LotSerSegmentWithNavigationPropertiesDto>
                (await _lotSerSegmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LotSerSegmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<LotSerSegment, LotSerSegmentDto>(await _lotSerSegmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetLotSerClassLookupAsync(LookupRequestDto input)
        {
            var query = (await _lotSerClassRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ClassID != null &&
                         x.ClassID.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<LotSerClass>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<LotSerClass>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(InventoryPermissions.LotSerSegments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _lotSerSegmentRepository.DeleteAsync(id);
        }

        [Authorize(InventoryPermissions.LotSerSegments.Create)]
        public virtual async Task<LotSerSegmentDto> CreateAsync(LotSerSegmentCreateDto input)
        {

            var lotSerSegment = await _lotSerSegmentManager.CreateAsync(
            input.LotSerClassId, input.SegmentID, input.AsgmentType, input.Value
            );

            return ObjectMapper.Map<LotSerSegment, LotSerSegmentDto>(lotSerSegment);
        }

        [Authorize(InventoryPermissions.LotSerSegments.Edit)]
        public virtual async Task<LotSerSegmentDto> UpdateAsync(Guid id, LotSerSegmentUpdateDto input)
        {

            var lotSerSegment = await _lotSerSegmentManager.UpdateAsync(
            id,
            input.LotSerClassId, input.SegmentID, input.AsgmentType, input.Value, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<LotSerSegment, LotSerSegmentDto>(lotSerSegment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerSegmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var lotSerSegments = await _lotSerSegmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.SegmentIDMin, input.SegmentIDMax, input.AsgmentType, input.Value);
            var items = lotSerSegments.Select(item => new
            {
                SegmentID = item.LotSerSegment.SegmentID,
                AsgmentType = item.LotSerSegment.AsgmentType,
                Value = item.LotSerSegment.Value,

                LotSerClass = item.LotSerClass?.ClassID,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "LotSerSegments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new LotSerSegmentExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public Task<List<LotSerSegmentDto>> GetListAllAttriDetail(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LotSerSegmentDto>> GetListAllClassDetail(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}