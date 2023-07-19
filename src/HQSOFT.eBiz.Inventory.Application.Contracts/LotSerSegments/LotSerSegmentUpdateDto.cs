using HQSOFT.eBiz.Inventory.LotSerSegments;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentUpdateDto : IHasConcurrencyStamp
    {
        public int SegmentID { get; set; }
        public Typeee AsgmentType { get; set; }
        public string? Value { get; set; }
        public Guid? LotSerClassId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}