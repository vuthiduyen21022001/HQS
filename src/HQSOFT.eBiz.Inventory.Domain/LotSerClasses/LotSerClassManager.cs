using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClassManager : DomainService
    {
        private readonly ILotSerClassRepository _lotSerClassRepository;

        public LotSerClassManager(ILotSerClassRepository lotSerClassRepository)
        {
            _lotSerClassRepository = lotSerClassRepository;
        }

        public async Task<LotSerClass> CreateAsync(
        string classID, string description, TrackingMethod trackingMethod, bool trackExpriationDate, bool requiredforDropShip, AssignMethod assignMethod, IssueMethod issueMethod, bool shareAutoIncremenitalValueBetwenAllClassItems, int autoIncremetalValue, bool autoGenerateNextNumber, int maxAutoNumbers)
        {
            Check.NotNullOrWhiteSpace(classID, nameof(classID));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNull(trackingMethod, nameof(trackingMethod));
            Check.NotNull(assignMethod, nameof(assignMethod));
            Check.NotNull(issueMethod, nameof(issueMethod));

            var lotSerClass = new LotSerClass(
             GuidGenerator.Create(),
             classID, description, trackingMethod, trackExpriationDate, requiredforDropShip, assignMethod, issueMethod, shareAutoIncremenitalValueBetwenAllClassItems, autoIncremetalValue, autoGenerateNextNumber, maxAutoNumbers
             );

            return await _lotSerClassRepository.InsertAsync(lotSerClass);
        }

        public async Task<LotSerClass> UpdateAsync(
            Guid id,
            string classID, string description, TrackingMethod trackingMethod, bool trackExpriationDate, bool requiredforDropShip, AssignMethod assignMethod, IssueMethod issueMethod, bool shareAutoIncremenitalValueBetwenAllClassItems, int autoIncremetalValue, bool autoGenerateNextNumber, int maxAutoNumbers, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(classID, nameof(classID));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNull(trackingMethod, nameof(trackingMethod));
            Check.NotNull(assignMethod, nameof(assignMethod));
            Check.NotNull(issueMethod, nameof(issueMethod));

            var lotSerClass = await _lotSerClassRepository.GetAsync(id);

            lotSerClass.ClassID = classID;
            lotSerClass.Description = description;
            lotSerClass.TrackingMethod = trackingMethod;
            lotSerClass.TrackExpriationDate = trackExpriationDate;
            lotSerClass.RequiredforDropShip = requiredforDropShip;
            lotSerClass.AssignMethod = assignMethod;
            lotSerClass.IssueMethod = issueMethod;
            lotSerClass.ShareAutoIncremenitalValueBetwenAllClassItems = shareAutoIncremenitalValueBetwenAllClassItems;
            lotSerClass.AutoIncremetalValue = autoIncremetalValue;
            lotSerClass.AutoGenerateNextNumber = autoGenerateNextNumber;
            lotSerClass.MaxAutoNumbers = maxAutoNumbers;

            lotSerClass.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _lotSerClassRepository.UpdateAsync(lotSerClass);
        }

    }
}