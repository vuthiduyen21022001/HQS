using HQSOFT.eBiz.Inventory.ReasonCodes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public interface IReasonCodeRepository : IRepository<ReasonCode, Guid>
    {
        Task<List<ReasonCode>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}