using HQSOFT.eBiz.Inventory.LotSerSegments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentManager : DomainService
    {
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;

        public LotSerSegmentManager(ILotSerSegmentRepository lotSerSegmentRepository)
        {
            _lotSerSegmentRepository = lotSerSegmentRepository;
        }

        public async Task<LotSerSegment> CreateAsync(
        Guid? lotSerClassId, int segmentID, Typeee asgmentType, string value)
        {
            Check.NotNull(asgmentType, nameof(asgmentType));

            var lotSerSegment = new LotSerSegment(
             GuidGenerator.Create(),
             lotSerClassId, segmentID, asgmentType, value
             );

            return await _lotSerSegmentRepository.InsertAsync(lotSerSegment);
        }

        public async Task<LotSerSegment> UpdateAsync(
            Guid id,
            Guid? lotSerClassId, int segmentID, Typeee asgmentType, string value, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(asgmentType, nameof(asgmentType));

            var lotSerSegment = await _lotSerSegmentRepository.GetAsync(id);

            lotSerSegment.LotSerClassId = lotSerClassId;
            lotSerSegment.SegmentID = segmentID;
            lotSerSegment.AsgmentType = asgmentType;
            lotSerSegment.Value = value;

            lotSerSegment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _lotSerSegmentRepository.UpdateAsync(lotSerSegment);
        }

    }
}