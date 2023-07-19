using HQSOFT.eBiz.Inventory.ReasonCodes;
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

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class EfCoreReasonCodeRepository : EfCoreRepository<InventoryDbContext, ReasonCode, Guid>, IReasonCodeRepository
    {
        public EfCoreReasonCodeRepository(IDbContextProvider<InventoryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<ReasonCode>> GetListAsync(
            string filterText = null,
            string reasonCodeID = null,
            string descr = null,
            ReasonCodeType? usage = null,
            string subMask = null,
            int? accountIDMin = null,
            int? accountIDMax = null,
            int? subIDMin = null,
            int? subIDMax = null,
            int? salesAcctIDMin = null,
            int? salesAcctIDMax = null,
            int? salesSubIDMin = null,
            int? salesSubIDMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, reasonCodeID, descr, usage, subMask, accountIDMin, accountIDMax, subIDMin, subIDMax, salesAcctIDMin, salesAcctIDMax, salesSubIDMin, salesSubIDMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ReasonCodeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string reasonCodeID = null,
            string descr = null,
            ReasonCodeType? usage = null,
            string subMask = null,
            int? accountIDMin = null,
            int? accountIDMax = null,
            int? subIDMin = null,
            int? subIDMax = null,
            int? salesAcctIDMin = null,
            int? salesAcctIDMax = null,
            int? salesSubIDMin = null,
            int? salesSubIDMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, reasonCodeID, descr, usage, subMask, accountIDMin, accountIDMax, subIDMin, subIDMax, salesAcctIDMin, salesAcctIDMax, salesSubIDMin, salesSubIDMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ReasonCode> ApplyFilter(
            IQueryable<ReasonCode> query,
            string filterText,
            string reasonCodeID = null,
            string descr = null,
            ReasonCodeType? usage = null,
            string subMask = null,
            int? accountIDMin = null,
            int? accountIDMax = null,
            int? subIDMin = null,
            int? subIDMax = null,
            int? salesAcctIDMin = null,
            int? salesAcctIDMax = null,
            int? salesSubIDMin = null,
            int? salesSubIDMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ReasonCodeID.Contains(filterText) || e.Descr.Contains(filterText) || e.SubMask.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(reasonCodeID), e => e.ReasonCodeID.Contains(reasonCodeID))
                    .WhereIf(!string.IsNullOrWhiteSpace(descr), e => e.Descr.Contains(descr))
                    .WhereIf(usage.HasValue, e => e.Usage == usage)
                    .WhereIf(!string.IsNullOrWhiteSpace(subMask), e => e.SubMask.Contains(subMask))
                    .WhereIf(accountIDMin.HasValue, e => e.AccountID >= accountIDMin.Value)
                    .WhereIf(accountIDMax.HasValue, e => e.AccountID <= accountIDMax.Value)
                    .WhereIf(subIDMin.HasValue, e => e.SubID >= subIDMin.Value)
                    .WhereIf(subIDMax.HasValue, e => e.SubID <= subIDMax.Value)
                    .WhereIf(salesAcctIDMin.HasValue, e => e.SalesAcctID >= salesAcctIDMin.Value)
                    .WhereIf(salesAcctIDMax.HasValue, e => e.SalesAcctID <= salesAcctIDMax.Value)
                    .WhereIf(salesSubIDMin.HasValue, e => e.SalesSubID >= salesSubIDMin.Value)
                    .WhereIf(salesSubIDMax.HasValue, e => e.SalesSubID <= salesSubIDMax.Value);
        }
    }
}