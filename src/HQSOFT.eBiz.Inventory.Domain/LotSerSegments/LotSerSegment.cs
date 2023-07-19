using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegment : FullAuditedAggregateRoot<Guid>
    {
        public virtual int SegmentID { get; set; }

        public virtual Typeee AsgmentType { get; set; }

        [CanBeNull]
        public virtual string? Value { get; set; }
        public Guid? LotSerClassId { get; set; }

        public LotSerSegment()
        {

        }

        public LotSerSegment(Guid id, Guid? lotSerClassId, int segmentID, Typeee asgmentType, string value)
        {

            Id = id;
            SegmentID = segmentID;
            AsgmentType = asgmentType;
            Value = value;
            LotSerClassId = lotSerClassId;
        }

    }
}