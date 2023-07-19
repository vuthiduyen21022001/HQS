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

namespace HQSOFT.eBiz.Inventory.LotSerClasses
{
    public class LotSerClass : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string ClassID { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual TrackingMethod TrackingMethod { get; set; }

        public virtual bool TrackExpriationDate { get; set; }

        public virtual bool RequiredforDropShip { get; set; }

        public virtual AssignMethod AssignMethod { get; set; }

        public virtual IssueMethod IssueMethod { get; set; }

        public virtual bool ShareAutoIncremenitalValueBetwenAllClassItems { get; set; }

        public virtual int AutoIncremetalValue { get; set; }

        public virtual bool AutoGenerateNextNumber { get; set; }

        public virtual int MaxAutoNumbers { get; set; }

        public LotSerClass()
        {

        }

        public LotSerClass(Guid id, string classID, string description, TrackingMethod trackingMethod, bool trackExpriationDate, bool requiredforDropShip, AssignMethod assignMethod, IssueMethod issueMethod, bool shareAutoIncremenitalValueBetwenAllClassItems, int autoIncremetalValue, bool autoGenerateNextNumber, int maxAutoNumbers)
        {

            Id = id;
            Check.NotNull(classID, nameof(classID));
            Check.NotNull(description, nameof(description));
            ClassID = classID;
            Description = description;
            TrackingMethod = trackingMethod;
            TrackExpriationDate = trackExpriationDate;
            RequiredforDropShip = requiredforDropShip;
            AssignMethod = assignMethod;
            IssueMethod = issueMethod;
            ShareAutoIncremenitalValueBetwenAllClassItems = shareAutoIncremenitalValueBetwenAllClassItems;
            AutoIncremetalValue = autoIncremetalValue;
            AutoGenerateNextNumber = autoGenerateNextNumber;
            MaxAutoNumbers = maxAutoNumbers;
        }

    }
}