using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HQSOFT.eBiz.Inventory.EntityFrameworkCore;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class EfCoreLotSerSegmentRepository : EfCoreRepository<InventoryDbContext, LotSerSegment, Guid>, ILotSerSegmentRepository
    {
        public EfCoreLotSerSegmentRepository(IDbContextProvider<InventoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<LotSerSegmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(lotSerSegment => new LotSerSegmentWithNavigationProperties
                {
                    LotSerSegment = lotSerSegment,
                    LotSerClass = dbContext.Set<LotSerClass>().FirstOrDefault(c => c.Id == lotSerSegment.LotSerClassId)
                }).FirstOrDefault();
        }

        public async Task<List<LotSerSegmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            Guid? lotSerClassId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, segmentIDMin, segmentIDMax, asgmentType, value, lotSerClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LotSerSegmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<LotSerSegmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from lotSerSegment in (await GetDbSetAsync())
                   join lotSerClass in (await GetDbContextAsync()).Set<LotSerClass>() on lotSerSegment.LotSerClassId equals lotSerClass.Id into lotSerClasses
                   from lotSerClass in lotSerClasses.DefaultIfEmpty()
                   select new LotSerSegmentWithNavigationProperties
                   {
                       LotSerSegment = lotSerSegment,
                       LotSerClass = lotSerClass
                   };
        }

        protected virtual IQueryable<LotSerSegmentWithNavigationProperties> ApplyFilter(
            IQueryable<LotSerSegmentWithNavigationProperties> query,
            string filterText,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            Guid? lotSerClassId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.LotSerSegment.Value.Contains(filterText))
                    .WhereIf(segmentIDMin.HasValue, e => e.LotSerSegment.SegmentID >= segmentIDMin.Value)
                    .WhereIf(segmentIDMax.HasValue, e => e.LotSerSegment.SegmentID <= segmentIDMax.Value)
                    .WhereIf(asgmentType.HasValue, e => e.LotSerSegment.AsgmentType == asgmentType)
                    .WhereIf(!string.IsNullOrWhiteSpace(value), e => e.LotSerSegment.Value.Contains(value))
                    .WhereIf(lotSerClassId != null && lotSerClassId != Guid.Empty, e => e.LotSerClass != null && e.LotSerClass.Id == lotSerClassId);
        }

        public async Task<List<LotSerSegment>> GetListAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
             Guid? lotSerClassId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, segmentIDMin, segmentIDMax, asgmentType, value, lotSerClassId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LotSerSegmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            Guid? lotSerClassId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, segmentIDMin, segmentIDMax, asgmentType, value, lotSerClassId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<LotSerSegment> ApplyFilter(
            IQueryable<LotSerSegment> query,
            string filterText,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            Guid? lotSerClassId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Value.Contains(filterText))
                    .WhereIf(segmentIDMin.HasValue, e => e.SegmentID >= segmentIDMin.Value)
                    .WhereIf(segmentIDMax.HasValue, e => e.SegmentID <= segmentIDMax.Value)
                    .WhereIf(asgmentType.HasValue, e => e.AsgmentType == asgmentType)
                    .WhereIf(!string.IsNullOrWhiteSpace(value), e => e.Value.Contains(value))
                    .WhereIf(lotSerClassId.HasValue, e => e.LotSerClassId == lotSerClassId);
        }
    }
}