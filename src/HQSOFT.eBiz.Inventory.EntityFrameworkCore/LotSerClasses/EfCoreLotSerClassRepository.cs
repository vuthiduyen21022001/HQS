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

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class EfCoreLotSerClassRepository : EfCoreRepository<InventoryDbContext, LotSerClass, Guid>, ILotSerClassRepository
    {
        public EfCoreLotSerClassRepository(IDbContextProvider<InventoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<LotSerClass>> GetListAsync(
            string filterText = null,
            string classID = null,
            string description = null,
            TrackingMethod? trackingMethod = null,
            bool? trackExpriationDate = null,
            bool? requiredforDropShip = null,
            AssignMethod? assignMethod = null,
            IssueMethod? issueMethod = null,
            bool? shareAutoIncremenitalValueBetwenAllClassItems = null,
            int? autoIncremetalValueMin = null,
            int? autoIncremetalValueMax = null,
            bool? autoGenerateNextNumber = null,
            int? maxAutoNumbersMin = null,
            int? maxAutoNumbersMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, classID, description, trackingMethod, trackExpriationDate, requiredforDropShip, assignMethod, issueMethod, shareAutoIncremenitalValueBetwenAllClassItems, autoIncremetalValueMin, autoIncremetalValueMax, autoGenerateNextNumber, maxAutoNumbersMin, maxAutoNumbersMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LotSerClassConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string classID = null,
            string description = null,
            TrackingMethod? trackingMethod = null,
            bool? trackExpriationDate = null,
            bool? requiredforDropShip = null,
            AssignMethod? assignMethod = null,
            IssueMethod? issueMethod = null,
            bool? shareAutoIncremenitalValueBetwenAllClassItems = null,
            int? autoIncremetalValueMin = null,
            int? autoIncremetalValueMax = null,
            bool? autoGenerateNextNumber = null,
            int? maxAutoNumbersMin = null,
            int? maxAutoNumbersMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, classID, description, trackingMethod, trackExpriationDate, requiredforDropShip, assignMethod, issueMethod, shareAutoIncremenitalValueBetwenAllClassItems, autoIncremetalValueMin, autoIncremetalValueMax, autoGenerateNextNumber, maxAutoNumbersMin, maxAutoNumbersMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<LotSerClass> ApplyFilter(
            IQueryable<LotSerClass> query,
            string filterText,
            string classID = null,
            string description = null,
            TrackingMethod? trackingMethod = null,
            bool? trackExpriationDate = null,
            bool? requiredforDropShip = null,
            AssignMethod? assignMethod = null,
            IssueMethod? issueMethod = null,
            bool? shareAutoIncremenitalValueBetwenAllClassItems = null,
            int? autoIncremetalValueMin = null,
            int? autoIncremetalValueMax = null,
            bool? autoGenerateNextNumber = null,
            int? maxAutoNumbersMin = null,
            int? maxAutoNumbersMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ClassID.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(classID), e => e.ClassID.Contains(classID))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(trackingMethod.HasValue, e => e.TrackingMethod == trackingMethod)
                    .WhereIf(trackExpriationDate.HasValue, e => e.TrackExpriationDate == trackExpriationDate)
                    .WhereIf(requiredforDropShip.HasValue, e => e.RequiredforDropShip == requiredforDropShip)
                    .WhereIf(assignMethod.HasValue, e => e.AssignMethod == assignMethod)
                    .WhereIf(issueMethod.HasValue, e => e.IssueMethod == issueMethod)
                    .WhereIf(shareAutoIncremenitalValueBetwenAllClassItems.HasValue, e => e.ShareAutoIncremenitalValueBetwenAllClassItems == shareAutoIncremenitalValueBetwenAllClassItems)
                    .WhereIf(autoIncremetalValueMin.HasValue, e => e.AutoIncremetalValue >= autoIncremetalValueMin.Value)
                    .WhereIf(autoIncremetalValueMax.HasValue, e => e.AutoIncremetalValue <= autoIncremetalValueMax.Value)
                    .WhereIf(autoGenerateNextNumber.HasValue, e => e.AutoGenerateNextNumber == autoGenerateNextNumber)
                    .WhereIf(maxAutoNumbersMin.HasValue, e => e.MaxAutoNumbers >= maxAutoNumbersMin.Value)
                    .WhereIf(maxAutoNumbersMax.HasValue, e => e.MaxAutoNumbers <= maxAutoNumbersMax.Value);
        }
    }
}