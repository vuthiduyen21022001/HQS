using HQSOFT.eBiz.Inventory.LotSerSegments;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentCreateDto
    {
        public int SegmentID { get; set; } = 0;
        public Typeee AsgmentType { get; set; } = ((Typeee[])Enum.GetValues(typeof(Typeee)))[0];
        public string? Value { get; set; }
        public Guid? LotSerClassId { get; set; }
    }
}