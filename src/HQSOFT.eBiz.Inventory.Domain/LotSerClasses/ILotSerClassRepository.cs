using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public interface ILotSerClassRepository : IRepository<LotSerClass, Guid>
    {
        Task<List<LotSerClass>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}