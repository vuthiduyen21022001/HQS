using HQSOFT.eBiz.Inventory.LotSerSegments;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int SegmentID { get; set; }
        public Typeee AsgmentType { get; set; }
        public string? Value { get; set; }
        public Guid? LotSerClassId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}