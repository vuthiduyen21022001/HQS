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
using HQSOFT.eBiz.Inventory.LotSerClasses;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.eBiz.Inventory.Shared;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{

    [Authorize(InventoryPermissions.LotSerClasses.Default)]
    public class LotSerClassesAppService : ApplicationService, ILotSerClassesAppService
    {
        private readonly IDistributedCache<LotSerClassExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ILotSerClassRepository _lotSerClassRepository;
        private readonly LotSerClassManager _lotSerClassManager;

        public LotSerClassesAppService(ILotSerClassRepository lotSerClassRepository, LotSerClassManager lotSerClassManager, IDistributedCache<LotSerClassExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _lotSerClassRepository = lotSerClassRepository;
            _lotSerClassManager = lotSerClassManager;
        }

        public virtual async Task<PagedResultDto<LotSerClassDto>> GetListAsync(GetLotSerClassesInput input)
        {
            var totalCount = await _lotSerClassRepository.GetCountAsync(input.FilterText, input.ClassID, input.Description, input.TrackingMethod, input.TrackExpriationDate, input.RequiredforDropShip, input.AssignMethod, input.IssueMethod, input.ShareAutoIncremenitalValueBetwenAllClassItems, input.AutoIncremetalValueMin, input.AutoIncremetalValueMax, input.AutoGenerateNextNumber, input.MaxAutoNumbersMin, input.MaxAutoNumbersMax);
            var items = await _lotSerClassRepository.GetListAsync(input.FilterText, input.ClassID, input.Description, input.TrackingMethod, input.TrackExpriationDate, input.RequiredforDropShip, input.AssignMethod, input.IssueMethod, input.ShareAutoIncremenitalValueBetwenAllClassItems, input.AutoIncremetalValueMin, input.AutoIncremetalValueMax, input.AutoGenerateNextNumber, input.MaxAutoNumbersMin, input.MaxAutoNumbersMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<LotSerClassDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<LotSerClass>, List<LotSerClassDto>>(items)
            };
        }

        public virtual async Task<LotSerClassDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<LotSerClass, LotSerClassDto>(await _lotSerClassRepository.GetAsync(id));
        }

        [Authorize(InventoryPermissions.LotSerClasses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _lotSerClassRepository.DeleteAsync(id);
        }

        [Authorize(InventoryPermissions.LotSerClasses.Create)]
        public virtual async Task<LotSerClassDto> CreateAsync(LotSerClassCreateDto input)
        {

            var lotSerClass = await _lotSerClassManager.CreateAsync(
            input.ClassID, input.Description, input.TrackingMethod, input.TrackExpriationDate, input.RequiredforDropShip, input.AssignMethod, input.IssueMethod, input.ShareAutoIncremenitalValueBetwenAllClassItems, input.AutoIncremetalValue, input.AutoGenerateNextNumber, input.MaxAutoNumbers
            );

            return ObjectMapper.Map<LotSerClass, LotSerClassDto>(lotSerClass);
        }

        [Authorize(InventoryPermissions.LotSerClasses.Edit)]
        public virtual async Task<LotSerClassDto> UpdateAsync(Guid id, LotSerClassUpdateDto input)
        {

            var lotSerClass = await _lotSerClassManager.UpdateAsync(
            id,
            input.ClassID, input.Description, input.TrackingMethod, input.TrackExpriationDate, input.RequiredforDropShip, input.AssignMethod, input.IssueMethod, input.ShareAutoIncremenitalValueBetwenAllClassItems, input.AutoIncremetalValue, input.AutoGenerateNextNumber, input.MaxAutoNumbers, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<LotSerClass, LotSerClassDto>(lotSerClass);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerClassExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _lotSerClassRepository.GetListAsync(input.FilterText, input.ClassID, input.Description, input.TrackingMethod, input.TrackExpriationDate, input.RequiredforDropShip, input.AssignMethod, input.IssueMethod, input.ShareAutoIncremenitalValueBetwenAllClassItems, input.AutoIncremetalValueMin, input.AutoIncremetalValueMax, input.AutoGenerateNextNumber, input.MaxAutoNumbersMin, input.MaxAutoNumbersMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<LotSerClass>, List<LotSerClassExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "LotSerClasses.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new LotSerClassExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}