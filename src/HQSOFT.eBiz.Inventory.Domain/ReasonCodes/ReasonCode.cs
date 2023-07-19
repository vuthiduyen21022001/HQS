using HQSOFT.eBiz.Inventory.ReasonCodes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using System.ComponentModel.DataAnnotations;

namespace HQSOFT.eBiz.Inventory.ReasonCodes
{
    public class ReasonCode : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string ReasonCodeID { get; set; }
        [CanBeNull]
        public virtual string? Descr { get; set; }

        public virtual ReasonCodeType Usage { get; set; }


        [StringLength(6, ErrorMessage = "Độ dài tên phải là 6 kí tự.")]
        [MaxLength(6)]
        [MinLength(6)]

        public virtual string? SubMask { get; set; }

        public virtual int? AccountID { get; set; }

        public virtual int? SubID { get; set; }

        public virtual int? SalesAcctID { get; set; }

        public virtual int? SalesSubID { get; set; }

        public ReasonCode()
        {

        }

        public ReasonCode(Guid id, string reasonCodeID, string descr, ReasonCodeType usage, string subMask, int accountID, int subID, int salesAcctID, int salesSubID)
        {

            Id = id;
            Check.NotNull(reasonCodeID, nameof(reasonCodeID));
            Check.NotNull(descr, nameof(descr));
            ReasonCodeID = reasonCodeID;
            Descr = descr;
            Usage = usage;
            SubMask = subMask;
            AccountID = accountID;
            SubID = subID;
            SalesAcctID = salesAcctID;
            SalesSubID = salesSubID;
        }

    }
}