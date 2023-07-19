using HQSOFT.eBiz.Inventory.ReasonCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCodeManager : DomainService
    {
        private readonly IReasonCodeRepository _reasonCodeRepository;

        public ReasonCodeManager(IReasonCodeRepository reasonCodeRepository)
        {
            _reasonCodeRepository = reasonCodeRepository;
        }

        public async Task<ReasonCode> CreateAsync(
        string reasonCodeID, string descr, ReasonCodeType usage, string subMask, int accountID, int subID, int salesAcctID, int salesSubID)
        {
            Check.NotNullOrWhiteSpace(reasonCodeID, nameof(reasonCodeID));
            Check.NotNullOrWhiteSpace(descr, nameof(descr));
            Check.NotNull(usage, nameof(usage));

            var reasonCode = new ReasonCode(
             GuidGenerator.Create(),
             reasonCodeID, descr, usage, subMask, accountID, subID, salesAcctID, salesSubID
             );

            return await _reasonCodeRepository.InsertAsync(reasonCode);
        }

        public async Task<ReasonCode> UpdateAsync(
            Guid id,
            string reasonCodeID, string descr, ReasonCodeType usage, string subMask, int accountID, int subID, int salesAcctID, int salesSubID, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(reasonCodeID, nameof(reasonCodeID));
            Check.NotNullOrWhiteSpace(descr, nameof(descr));
            Check.NotNull(usage, nameof(usage));

            var reasonCode = await _reasonCodeRepository.GetAsync(id);

            reasonCode.ReasonCodeID = reasonCodeID;
            reasonCode.Descr = descr;
            reasonCode.Usage = usage;
            reasonCode.SubMask = subMask;
            reasonCode.AccountID = accountID;
            reasonCode.SubID = subID;
            reasonCode.SalesAcctID = salesAcctID;
            reasonCode.SalesSubID = salesSubID;

            reasonCode.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _reasonCodeRepository.UpdateAsync(reasonCode);
        }

    }
}